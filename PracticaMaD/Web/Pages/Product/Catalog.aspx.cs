using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Windows.Forms;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class Catalog : SpecificCulturePage
    {
        int startIndex, index;
        int count = 4;
        int dateCell=2, priceCell = 3, addCell = 4;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            string dateFormat = "MM/dd/yyyy";
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