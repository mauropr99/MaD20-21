using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class Catalog : SpecificCulturePage
    {
        int startIndex, index;
        int count = 4;
        int dateCell = 2, priceCell = 3, addCell = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                /* Get Start Index */
                try
                {
                    startIndex = int.Parse(Request.Params.Get("startIndex"));
                    index = int.Parse(Request.Params.Get("index"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                    index = 0;
                }


                LoadPage();
            }
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

            List<Category> categoryList = productService.ViewAllCategories();

            DropDownCategoryList.Items.Clear();

            DropDownCategoryList.Items.Insert(0, "All categories");

            foreach (Category category in categoryList)
            {
                DropDownCategoryList.Items.Add(category.name);
            }

            DropDownCategoryList.SelectedIndex = index;
       
            DropDownCategoryList.Visible = true;
        }

        protected void LoadCatalog(IProductService productService)
        {
            string dateFormat = GetFormat();
            ProductBlock productBlock;

            string productName = txtProductName.Text;

            if (DropDownCategoryList.SelectedValue == "All categories")
            {
                productBlock = productService.ViewCatalog(productName, startIndex, count);
            }
            else
            {
                productBlock = productService.ViewCatalog(productName, DropDownCategoryList.SelectedValue, startIndex, count);
            }


            GridViewCatalog.DataSource = productBlock.Products;

            GridViewCatalog.DataBind();


            IShoppingService shoppingService = SessionManager.GetShoppingService();

            for (int i = 0; i < GridViewCatalog.Rows.Count; i++)
            {
                GridViewCatalog.Rows[i].Cells[dateCell].Text = productBlock.Products[i].ReleaseDate.ToString(dateFormat);
                GridViewCatalog.Rows[i].Cells[priceCell].Text = productBlock.Products[i].Price.ToString("C2");
                if (productBlock.Products[i].Stock == 0) GridViewCatalog.Rows[i].Cells[addCell].Text = GetLocalResourceObject("outStock").ToString();
            }


            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            if (productBlock.ExistMoreProducts)
            {
                string url =
                    "/Pages/Product/Catalog.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&index=" + index;

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/Product/Catalog.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&index=" + index;

                lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

        }

        private string GetFormat()
        {
            string dateFormat = "MM/dd/yyyy";

            //We can access the locale information only if the user is authenticated
            if (SessionManager.IsUserAuthenticated(Context))
            {
                //Changing the date format...
                Locale locale = SessionManager.GetLocale(Context);

                switch (locale.Country)
                {
                    case "ES":
                        dateFormat = "dd/MM/yyyy";
                        break;
                    case "US":
                        dateFormat = "MM/dd/yyyy";
                        break;

                    default:
                        dateFormat = "MM/dd/yyyy";
                        break;
                }
            }

            return dateFormat;
        }

        protected void BtnViewCatalog_Click(object sender, EventArgs e)
        {
            startIndex = 0;
            index = DropDownCategoryList.SelectedIndex;
            LoadPage();
        }

        protected void GridViewCatalog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {

                try
                {
                    IShoppingService shoppingService = SessionManager.GetShoppingService();

                    int index = Convert.ToInt32(e.CommandArgument);
                    long productId = long.Parse(GridViewCatalog.DataKeys[index].Values[0].ToString());

                    shoppingService.AddToShoppingCart(productId);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);

                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
                }

            }
        }

    }
}