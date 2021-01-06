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
        int labelCell = 1;
        static DataTable dt = new DataTable();
        static List<string> labels = new List<string>();

        private  DataTable Dt { get => dt; set => dt = value; }
        private  List<string> Labels { get => labels; set => labels = value; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                string column = GetLocalResourceObject("label").ToString();
                labels = new List<string>();
                dt = new DataTable();
                dt.Clear();
                dt.Columns.Add(column);

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

            commentService.NewComment(userSession.UserId, productId, comment, Labels);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));

        }

        protected void GridViewComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);

            try
            {

                switch (e.CommandName)
                {
                    case "Deletelabel":
                        dt.Rows.RemoveAt(index);
                        labels.Remove(HttpUtility.HtmlDecode(GridViewLabels.Rows[index].Cells[labelCell].Text.Trim()));
                        GridViewLabels.DataSource = dt;
                        GridViewLabels.DataBind();
                        break;
                }

            }
            catch { }

        }

        protected void BtnAddLabel_Click(object sender, EventArgs e)
        {
            string column = GetLocalResourceObject("label").ToString();
            string label = HttpUtility.HtmlDecode(this.txtLabelContent.Text.Trim());
            
            DataRow dr;
            bool exists = false;
            foreach (GridViewRow row in this.GridViewLabels.Rows)
            {
                if(!exists) exists = HttpUtility.HtmlDecode(row.Cells[labelCell].Text.Trim()) == label;
            }

            if (!exists && label != "")
            {
                dr = Dt.NewRow();
                dr[column] = label;
                Labels.Add(label);
                Dt.Rows.Add(dr);

                this.GridViewLabels.DataSource = Dt;
                this.GridViewLabels.DataBind();
            }

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