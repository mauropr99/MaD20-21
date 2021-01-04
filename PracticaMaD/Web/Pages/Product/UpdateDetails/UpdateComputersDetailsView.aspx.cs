using System;
using System.Text.RegularExpressions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Web.Pages.Product
{

    public partial class UpdateComputersDetailsView : SpecificCulturePage
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
                    Computer computer = productService.FindComputer(productId);

                    //Fill place holders
                    txtComputerNameContent.Text = computer.product_name;
                    txtBrandContent.Text = computer.brand;
                    txtPriceContent.Text = computer.price.ToString("C2");
                    txtStockContent.Text = computer.stock.ToString();
                    txtProcessorContent.Text = computer.processor;
                    txtOperatingSystemContent.Text = computer.os;

                }
                catch (ArgumentNullException)
                {
                }

                ViewState["RefUrl"] = Request.UrlReferrer.ToString();

            }
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
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
                    Computer computer = productService.FindComputer(productId);
                    computer.product_name = txtComputerNameContent.Text;
                    computer.brand = txtBrandContent.Text;
                    computer.price = decimal.Parse(Regex.Match(txtPriceContent.Text, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
                    computer.stock = Int32.Parse(txtStockContent.Text);
                    computer.processor = txtProcessorContent.Text;
                    computer.os = txtOperatingSystemContent.Text;
                    productService.UpdateComputer(computer);
                    Response.Redirect("~/Pages/Product/Catalog.aspx");
                }
            }
            catch (ArgumentNullException)
            {
            }

        }
    }

}