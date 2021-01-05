﻿using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CommentList : SpecificCulturePage
    {
        long productId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int startIndex = 0, count = 5, dateCell = 2;
            string dateFormat = GetFormat();
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            CommentBlock commentBlock = null;

            if (!IsPostBack)
            {

                /* Get Start Index */
                try
                {
                    productId = Int64.Parse(Request.Params.Get("productId"));
                    startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

            }

            commentBlock = commentService.ViewComments(productId, startIndex, count);

            if(commentBlock.Comments.Count == 0)
            {
                this.txtEmptyComment.Visible = true;
            }
            else
            {
                this.txtEmptyComment.Visible = false;
            }

            GridViewComments.DataSource = commentBlock.Comments;
            GridViewComments.DataBind();

            for (int i = 0; i < GridViewComments.Rows.Count; i++)
            {
                GridViewComments.Rows[i].Cells[dateCell].Text = commentBlock.Comments[i].Date.ToString(dateFormat);
            }

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            if (commentBlock.ExistMoreComments)
            {
                String url =
                    "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&productId=" + productId.ToString() + "&categoryName="+ Request.Params.Get("categoryName"); 

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/Comment/CommentList.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&productId=" + productId.ToString() + "&categoryName=" + Request.Params.Get("categoryName"); 

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
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

    }

}