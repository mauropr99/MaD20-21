using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Product
{
    public partial class BookDetailsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long productId;
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();


            /* Get Start Index */
            try
            {
                productId = long.Parse(Request.Params.Get("productId"));
                Book book = productService.FindBook(productId);

                //Fill place holders
                lblAuthorContent.Text = book.author;
                lblGenreContent.Text = book.genre;
                lblIsbnCodeContent.Text = book.isbnCode;
                lblPriceContent.Text = book.price.ToString("C2");
                lblReleaseDateContent.Text = book.releaseDate.ToString("MM/dd/yyyy");
                lblStockContent.Text = book.stock.ToString();
                lblTitleContent.Text = book.product_name;


                if(book.stock == 0 || !SessionManager.IsUserAuthenticated(Context))
                {
                    lblQuantity.Visible = false;
                    DropDownListQuantity.Visible = false;
                    btnAddToShoppingCart.Visible = false;
                } else {
                    /*If the user is authenticated we offer 
                     - A drop down list with the quantity of objects to add to the shopping cart.
                     - The option of adding the product to the shopping cart.
                     */

                    int limit = (book.stock < 10) ? book.stock : 10;
                    for (int i = 0; i < limit; i++)
                    {
                        DropDownListQuantity.Items[i].Enabled = true;
                    }

                    lblQuantity.Visible = false;
                    DropDownListQuantity.Visible = true;
                    btnAddToShoppingCart.Visible = true;

                }


            }
            catch (ArgumentNullException)
            {
            }


            

            

        }
    }


}