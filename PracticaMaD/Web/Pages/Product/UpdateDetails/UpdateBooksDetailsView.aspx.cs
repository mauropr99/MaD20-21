using System;
using System.Text.RegularExpressions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
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
                    Book book = productService.FindBook(productId);

                    //Fill place holders
                    lblTitleContent.Text = book.product_name;
                    lblAuthorContent.Text = book.author;
                    lblPriceContent.Text = book.price.ToString("C2");
                    lblStockContent.Text = book.stock.ToString();
                    lblGenreContent.Text = book.genre;

                }
                catch (ArgumentNullException)
                {
                }
            }
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
                    book.product_name = lblTitleContent.Text;
                    book.author = lblAuthorContent.Text;
                    book.price = decimal.Parse(Regex.Match(lblPriceContent.Text, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
                    book.stock = Int32.Parse(lblStockContent.Text);
                    book.genre = lblGenreContent.Text;
                    productService.UpdateBook(book);
                    Response.Redirect("~/Pages/Product/Catalog.aspx");
                }
            }
            catch (ArgumentNullException)
            {
            }

        }
    }

}