using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class Catalog : SpecificCulturePage
    {
        int startIndex, index;
        int count = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                index = Int32.Parse(Request.Params.Get("index"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
                index = 0;
            }


            LoadPage();
        }

        protected void LoadPage()
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();
            
            LoadDropDownCategoryList(productService, index);

            LoadCatalog(productService);
        }
        protected void LoadDropDownCategoryList(IProductService productService, int index)
        {
            if (DropDownCategoryList.Items.Count == 0)
            {
                List<Category> categoryList = productService.ViewAllCategories();

                this.DropDownCategoryList.Items.Clear();

                this.DropDownCategoryList.Items.Insert(0, "All categories");

                foreach (Category category in categoryList)
                {
                    this.DropDownCategoryList.Items.Add(category.name);
                }

                this.DropDownCategoryList.SelectedIndex = index;
            }

            this.DropDownCategoryList.Visible = true;
        }

        protected void LoadCatalog(IProductService productService)
        {
            ProductBlock productBlock;

            String productName = txtProductName.Text;

            if (DropDownCategoryList.SelectedValue == "All categories")
            {
                productBlock = productService.ViewCatalog(productName, startIndex, count);
            }
            else
            {
                productBlock = productService.ViewCatalog(productName, DropDownCategoryList.SelectedValue, startIndex, count);
            }

            this.GridViewCatalog.DataSource = productBlock.Products;

            this.GridViewCatalog.DataBind();

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            if (productBlock.ExistMoreProducts)
            {
                String url =
                    "/Pages/Product/Catalog.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&index=" + index;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/Product/Catalog.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&index=" + index;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

        }

        protected void GridViewCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnViewCatalog_Click(object sender, EventArgs e)
        {
            startIndex = 0;
            index = DropDownCategoryList.SelectedIndex;
            LoadPage();
        }

    }
}