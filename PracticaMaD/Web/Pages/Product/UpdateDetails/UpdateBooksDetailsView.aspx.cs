using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

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
                    errorPrice.Visible = false;

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
                    book.stock = int.Parse(txtStockContent.Text);
                    book.genre = txtGenreContent.Text;
                    productService.UpdateBook(book);
                    Response.Redirect("~/Pages/Product/Catalog.aspx");
                    try
                    {
                        string culture;
                        if (SessionManager.IsUserAuthenticated(Context))
                        {
                            Locale locale = SessionManager.GetLocale(Context);

                            culture = locale.Language + "-" + locale.Country;
                        }
                        else
                        {
                            culture = "en-US";
                        }
                        CultureInfo cultureInfo;
                        try
                        {
                            cultureInfo = new CultureInfo(culture);
                        }
                        catch (ArgumentException)
                        {
                            cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                        }
                        book.price = Decimal.Parse(txtPriceContent.Text, cultureInfo);
                        if (book.price < 0)
                        {
                            errorPrice.Visible = true;
                        }
                        else
                        {
                            productService.UpdateBook(book);
                            Response.Redirect("~/Pages/Product/Catalog.aspx");
                        }
                    }
                    catch
                    {
                        errorPrice.Visible = true;
                    }
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