using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class UpdateComment : SpecificCulturePage
    {
        ICommentService commentService;
        int labelCell = 1;
        static DataTable dt = new DataTable();
        static List<string> labels = new List<string>();

        private DataTable Dt { get => dt; set => dt = value; }
        private List<string> Labels { get => labels; set => labels = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            commentService = iocManager.Resolve<ICommentService>();

            long commentId = long.Parse(Request.Params.Get("commentId"));

            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                string column = GetLocalResourceObject("label").ToString();
                CommentDetails comment = commentService.FindComment(commentId);

                txtCommentContent.Text = comment.Text;

                labels = comment.Labels;
                dt = new DataTable();
                dt.Clear();
                dt.Columns.Add(column);
                DataRow dr;
                foreach (string label in Labels)
                {
                    dr = dt.NewRow();
                    dr[column] = label;
                    dt.Rows.Add(dr);
                }

                GridViewLabels.DataSource = dt;
                GridViewLabels.DataBind();
            }


        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {
            UserSession userSession =
               (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            commentService.UpdateComment(userSession.UserId, long.Parse(Request.Params.Get("commentId")), txtCommentContent.Text, labels);

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
            string label = HttpUtility.HtmlDecode(txtLabelContent.Text.Trim().ToLower());

            DataRow dr;
            bool exists = false;
            foreach (GridViewRow row in GridViewLabels.Rows)
            {
                if (!exists) exists = HttpUtility.HtmlDecode(row.Cells[labelCell].Text.Trim()) == label;
            }

            if (!exists && label != "")
            {
                dr = Dt.NewRow();
                dr[column] = label;
                Labels.Add(label);
                Dt.Rows.Add(dr);

                GridViewLabels.DataSource = Dt;
                GridViewLabels.DataBind();
            }

            txtLabelContent.Text = "";
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }
}