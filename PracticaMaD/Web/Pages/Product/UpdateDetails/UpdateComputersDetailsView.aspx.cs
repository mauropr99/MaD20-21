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
                    lblComputerNameContent.Text = computer.product_name;
                    lblBrandContent.Text = computer.brand;
                    lblPriceContent.Text = computer.price.ToString("C2");
                    lblStockContent.Text = computer.stock.ToString();
                    lblProcessorContent.Text = computer.processor;
                    lblOperatingSystemContent.Text = computer.os;

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
                    Computer computer = productService.FindComputer(productId);
                    computer.product_name = lblComputerNameContent.Text;
                    computer.brand = lblBrandContent.Text;
                    computer.price = decimal.Parse(Regex.Match(lblPriceContent.Text, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
                    computer.stock = Int32.Parse(lblStockContent.Text);
                    computer.processor = lblProcessorContent.Text;
                    computer.os = lblOperatingSystemContent.Text;
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