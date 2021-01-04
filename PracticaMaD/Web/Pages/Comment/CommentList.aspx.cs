using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CommentList : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long productId = Int64.Parse(Request.Params.Get("productId"));
            int startIndex = 0, count = 5;
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            CommentBlock commentBlock = null;

            if (!IsPostBack)
            {

                /* Get Start Index */
                try
                {
                    startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

            }
            DataListComments.DataSource = commentBlock.Comments;
            DataListComments.DataBind();


            commentBlock = commentService.ViewComments(productId,startIndex,count);

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            if (commentBlock.ExistMoreComments)
            {
                String url =
                    "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }
        }


    }
}