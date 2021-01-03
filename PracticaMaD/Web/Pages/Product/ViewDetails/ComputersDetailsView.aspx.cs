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
    public partial class ComputersDetailsView : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string format = "MM/dd/yyyy";
            long productId;
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();


            /* Get Start Index */
            try
            {
                productId = long.Parse(Request.Params.Get("productId"));
                Computer computer = productService.FindComputer(productId);


                if (computer.stock == 0 || !SessionManager.IsUserAuthenticated(Context))
                {
                    lblQuantity.Visible = false;
                    DropDownListQuantity.Visible = false;
                    btnAddToShoppingCart.Visible = false;
                }
                else
                {
                    /*If the user is authenticated we have to:
                     - Offer a drop down list with the quantity of objects to add to the shopping cart.
                     - Offer the option of adding the product to the shopping cart.
                     - Change the date format
                     */

                    int limit = (computer.stock < 10) ? computer.stock : 10;
                    for (int i = 0; i < limit; i++)
                    {
                        DropDownListQuantity.Items[i].Enabled = true;
                    }

                    lblQuantity.Visible = true;
                    DropDownListQuantity.Visible = true;
                    btnAddToShoppingCart.Visible = true;

                    if (SessionManager.IsUserAuthenticated(Context))
                    {
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


                }
                //Fill place holders
                lblBrandContent.Text = computer.brand;
                lblComputerNameContent.Text = computer.product_name;
                lblOperatingSystemContent.Text = computer.os;
                lblProcessorContent.Text = computer.processor;
                lblStockContent.Text = computer.stock.ToString();
                lblReleaseDateContent.Text = computer.releaseDate.ToString(format);
                lblReleaseDateContent.Text = computer.releaseDate.ToString(format);
                lblPriceContent.Text = computer.price.ToString("C2");



            }
            catch (ArgumentNullException)
            {
            }

            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
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
    }

}