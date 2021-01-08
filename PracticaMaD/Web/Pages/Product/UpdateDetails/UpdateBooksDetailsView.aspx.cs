using System;
using System.Text.RegularExpressions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Web.Pages.Product
{

    public partial class UpdateBooksDetailsView : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                try
                {
                    long productId = long.Parse(Request.Params.Get("productId"));
                    ICommentService commentService = iocManager.Resolve<ICommentService>();
                    UserSession userSession =
                        (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];
                    if (userSession != null) btnNewComment.Visible = !(commentService.UserAlreadyCommented(productId, userSession.UserId));
                    Book book = productService.FindBook(productId);
                    linkViewComment.Visible = productService.HasComments(productId);

                    //Fill place holders
                    txtTitleContent.Text = book.product_name;
                    txtAuthorContent.Text = book.author;
                    txtPriceContent.Text = book.price.ToString("C2");
                    txtStockContent.Text = book.stock.ToString();
                    txtGenreContent.Text = book.genre;

                }
                catch (ArgumentNullException)
                {
                }
           
            }
        }
    

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Product/Catalog.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();
            try
            {
                if (Page.IsValid)
                {
                    long productId = long.Parse(Request.Params.Get("productId"));
                    Book book = productService.FindBook(productId);
                    book.product_name = txtTitleContent.Text;
                    book.author = txtAuthorContent.Text;
                    book.price = decimal.Parse(Regex.Match(txtPriceContent.Text, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
                    book.stock = Int32.Parse(txtStockContent.Text);
                    book.genre = txtGenreContent.Text;
                    productService.UpdateBook(book);
                    Response.Redirect("~/Pages/Product/Catalog.aspx");
                }
            }
            catch (ArgumentNullException)
            {
            }

        }

        protected void BtnNewComment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Comment/CommentAdd.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName"));
        }

        protected void Book_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));
        }
    }

}