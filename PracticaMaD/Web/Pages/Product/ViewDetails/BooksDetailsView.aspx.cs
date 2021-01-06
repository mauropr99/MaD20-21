using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;



namespace Web.Pages.Product
{
    public partial class BooksDetailsView : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string format = "MM/dd/yyyy";
            long productId;
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            try
            {
                productId = long.Parse(Request.Params.Get("productId"));
                Book book = productService.FindBook(productId);
                linkViewComment.Visible = productService.HasComments(productId);

                if (book.stock == 0 || !SessionManager.IsUserAuthenticated(Context))
                {
                    lblQuantity.Visible = false;
                    DropDownListQuantity.Visible = false;
                    btnAddToShoppingCart.Visible = false;
                }
                else
                {
                    /*If the user is authenticated we offer 
                     - A drop down list with the quantity of objects to add to the shopping cart.
                     - The option of adding the product to the shopping cart.
                     */

                    int limit = (book.stock < 10) ? book.stock : 10;
                    for (int i = 0; i < limit; i++)
                    {
                        DropDownListQuantity.Items[i].Enabled = true;
                    }

                    lblQuantity.Visible = true;
                    DropDownListQuantity.Visible = true;
                    btnAddToShoppingCart.Visible = true;

                    //Changing the date format...
                    Locale locale = SessionManager.GetLocale(Context);

                    switch (locale.Country)
                    {
                        case "ES":
                            format = "dd/MM/yyyy";
                            break;
                        case "US":
                            format = "MM/dd/yyyy";
                            break;

                        default:
                            format = "MM/dd/yyyy";
                            break;
                    }

                }
                //Fill place holders
                lblAuthorContent.Text = book.author;
                lblGenreContent.Text = book.genre;
                lblIsbnCodeContent.Text = book.isbnCode;
                lblPriceContent.Text = book.price.ToString("C2");
                lblReleaseDateContent.Text = book.releaseDate.ToString(format);
                lblStockContent.Text = book.stock.ToString();
                lblTitleContent.Text = book.product_name;


            }
            catch (ArgumentNullException)
            {
            }

        
            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }

        }

        protected void btnAddToShoppingCart_Click(object sender, EventArgs e)
        {
            try
            {
                IShoppingService shoppingService = SessionManager.GetShoppingService();

                long productId = long.Parse(Request.Params.Get("productId"));

                shoppingService.AddToShoppingCart(productId, short.Parse(DropDownListQuantity.SelectedValue));

                Response.Redirect("~/Pages/Shopping/ShoppingCart.aspx");

            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null || refUrl.ToString().Contains("Comment")) Response.Redirect("~/Pages/Product/Catalog.aspx");
            if (refUrl != null) Response.Redirect((string)refUrl);
        }

        protected void Book_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));
            }
            catch { }
        }
    }
}