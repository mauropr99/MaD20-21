using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CommentAdd : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void BtnAddComment_Click(object sender, EventArgs e)
        {
            UserSession userSession =
                    (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            if (userSession == null) Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));


            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            //2 Obtener el servicio

            ICommentService commentService = iocManager.Resolve<ICommentService>();


            //3 Llamar al caso de uso (lectura de parámetros y actualización de la vista)

            String comment = txtCommentAdd.Text;

            long productId = long.Parse(Request.Params.Get("productId"));

            List<String> labels = new List<String>();

            foreach (GridViewRow row in this.GridViewLabels.Rows)
            {
                labels.Add(row.Cells[0].Text);
            }

            commentService.NewComment(userSession.UserId, productId, comment, labels);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));

        }

        protected void BtnAddLabel_Click(object sender, EventArgs e)
        {
            string column = GetLocalResourceObject("label").ToString();
            string label = HttpUtility.HtmlDecode(this.txtLabelContent.Text.Trim());
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(column);
            DataRow dr;
            bool exists = false;
            foreach (GridViewRow row in this.GridViewLabels.Rows)
            {
                if(!exists) exists = HttpUtility.HtmlDecode(row.Cells[0].Text.Trim()) == label;
                dr = dt.NewRow();
                dr[column] = HttpUtility.HtmlDecode(row.Cells[0].Text.Trim());
                dt.Rows.Add(dr);
            }

            if (!exists)
            {
                dr = dt.NewRow();
                dr[column] = label;
                dt.Rows.Add(dr);
            }

            this.GridViewLabels.DataSource = dt;
            this.GridViewLabels.DataBind();
 
            this.txtLabelContent.Text = "";
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }
}