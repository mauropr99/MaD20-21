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
    public partial class ComputerDetailsView : SpecificCulturePage
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
                Computer computer = productService.FindComputer(productId);

                //Fill place holders
                lblBrandContent.Text = computer.brand;
                lblComputerNameContent.Text = computer.product_name;
                lblOperatingSystemContent.Text = computer.os;
                lblPriceContent.Text = computer.price.ToString("C2");
                lblProcessorContent.Text = computer.processor;
                lblReleaseDateContent.Text = computer.releaseDate.ToString("MM/dd/yyyy");
                lblStockContent.Text = computer.stock.ToString();


                if (computer.stock == 0 || !SessionManager.IsUserAuthenticated(Context))
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

                    int limit = (computer.stock < 10) ? computer.stock : 10;
                    for (int i = 0; i < limit; i++)
                    {
                        DropDownListQuantity.Items[i].Enabled = true;
                    }

                    lblQuantity.Visible = true;
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