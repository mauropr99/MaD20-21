﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CommentList : SpecificCulturePage
    {
        ICommentService commentService;

        protected void Page_Load(object sender, EventArgs e)
        {
            long productId = 0;
            int startIndex = 0, count = 5;
            int commentCell = 1, dateCell = 2, deleteCell = 3, updateCell = 4;
            string dateFormat = GetFormat();

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            commentService = iocManager.Resolve<ICommentService>();
            CommentBlock commentBlock = null;

            UserSession userSession =
               (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            if (!IsPostBack)
            {

                /* Get Start Index */
                try
                {
                    productId = long.Parse(Request.Params.Get("productId"));
                    startIndex = int.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

                commentBlock = commentService.ViewComments(productId, startIndex, count);

                if (commentBlock.Comments.Count == 0)
                {
                    txtEmptyComment.Visible = true;
                }
                else
                {
                    txtEmptyComment.Visible = false;
                }

                GridViewComments.DataSource = commentBlock.Comments;
                GridViewComments.DataBind();


                for (int i = 0; i < GridViewComments.Rows.Count; i++)
                {
                    GridViewComments.Rows[i].Cells[dateCell].Text = commentBlock.Comments[i].Date.ToString(dateFormat);

                    GridViewComments.Rows[i].Cells[deleteCell].Visible = false;
                    GridViewComments.Rows[i].Cells[updateCell].Visible = false;

                    if (commentBlock.Comments[i].Labels.Count > 0)
                    {
                        GridViewComments.Rows[i].Cells[commentCell].Text = "<b><br />" + string.Join(",", commentBlock.Comments[i].Labels) + "</b > <br /><br />" + GridViewComments.Rows[i].Cells[commentCell].Text;
                    }

                    if (SessionManager.IsUserAuthenticated(Context))
                    {
                        if (userSession.UserId == long.Parse(GridViewComments.DataKeys[i].Values[1].ToString()))
                        {
                            GridViewComments.Rows[i].Cells[deleteCell].Visible = true;
                            GridViewComments.Rows[i].Cells[updateCell].Visible = true;
                        }
                    }


                }

                lnkPrevious.Visible = false;
                lnkNext.Visible = false;

                if (commentBlock.ExistMoreComments)
                {
                    string url =
                        "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                        count + "&productId=" + productId.ToString() + "&categoryName=" + Request.Params.Get("categoryName");

                    lnkNext.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    lnkNext.Visible = true;
                }

                if ((startIndex - count) >= 0)
                {
                    string url =
                        "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                        count + "&productId=" + productId.ToString() + "&categoryName=" + Request.Params.Get("categoryName");

                    lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    lnkPrevious.Visible = true;
                }

            }

        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = "~/Pages/Product/DetailsViewController.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName");
            Response.Redirect((string)refUrl);
        }

        private string GetFormat()
        {
            string dateFormat = "MM/dd/yyyy";

            //We can access the locale information only if the user is authenticated
            if (SessionManager.IsUserAuthenticated(Context))
            {
                //Changing the date format...
                Locale locale = SessionManager.GetLocale(Context);

                switch (locale.Country)
                {
                    case "ES":
                        dateFormat = "dd/MM/yyyy";
                        break;
                    case "US":
                        dateFormat = "MM/dd/yyyy";
                        break;

                    default:
                        dateFormat = "MM/dd/yyyy";
                        break;
                }
            }

            return dateFormat;
        }

        protected void GridViewComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            UserSession userSession =
               (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            int index = Convert.ToInt32(e.CommandArgument);
            long commentId = long.Parse(GridViewComments.DataKeys[index].Values[0].ToString());


            try
            {

                switch (e.CommandName)
                {
                    case "DeleteComment":
                        commentService.RemoveComment(userSession.UserId, commentId);
                        break;

                    case "EditComment":
                        Response.Redirect("~/Pages/Comment/UpdateComment.aspx?commentId=" + commentId + "&productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName"));
                        break;
                }

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch { }

        }

    }

}