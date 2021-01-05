using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class UpdateComment : SpecificCulturePage
    {
        ICommentService commentService;
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            commentService = iocManager.Resolve<ICommentService>();

            long commentId = long.Parse(Request.Params.Get("commentId"));

            if (!IsPostBack)
            {
                CommentDetails comment = commentService.FindComment(commentId);

                txtCommentContent.Text = comment.Text;

            }



        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {
            UserSession userSession =
               (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            List<String> labels = new List<String>();
            commentService.UpdateComment(userSession.UserId, long.Parse(Request.Params.Get("commentId")), txtCommentContent.Text, labels);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));
        }
    }
}