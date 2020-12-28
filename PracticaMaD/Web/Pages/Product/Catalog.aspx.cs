using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using System.Collections.Generic;
using Web.Properties;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class Catalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            //2 Obtener el servicio

            IProductService productService = iocManager.Resolve<IProductService>();

            //Llamar a los casos de uso
            List<Category> categoryList = productService.ViewAllCategories();

            //Cargamos las categorías en la DropDownList la primera vez que carguemos la página
            if (DropDownCategoryList.Items.Count == 0)
            {
                this.DropDownCategoryList.Items.Clear();

                this.DropDownCategoryList.Items.Insert(0, "All categories");

                foreach (Category category in categoryList)
                {
                    this.DropDownCategoryList.Items.Add(category.name);
                }
            }
            

            this.DropDownCategoryList.Visible = true;





        }

        protected void btnViewCatalog_Click(object sender, EventArgs e)
        {
            int startIndex, count = 5;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }



            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager) Application["managerIoC"];

            //2 Obtener el servicio

            IProductService productService = iocManager.Resolve<IProductService>();

            //3 Llamar al caso de uso (lectura de parámetros y actualización de la vista)
            ProductBlock productBlock;


            String productName = txtProductName.Text;


            if (DropDownCategoryList.SelectedIndex == 0)
            {
                productBlock = productService.ViewCatalog(productName, startIndex, count);
            } else
            {
                productBlock = productService.ViewCatalog(productName, DropDownCategoryList.SelectedValue, startIndex, count);
            }
            this.DropDownCategoryList.SelectedIndex = DropDownCategoryList.SelectedIndex;


            //Cargamos los resultados en la lista de productos
            LoadCatalog(productBlock);

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/Product/Catalog.aspx" +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (productBlock.ExistMoreProducts)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/Product/Catalog.aspx" +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }


        protected void LoadCatalog(ProductBlock productBlock)
        {
            //Cargamos los datos en el grid
            this.GridViewCatalog.DataSource = productBlock.Products;

            this.GridViewCatalog.DataBind();


        }

        

        protected void ListProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}