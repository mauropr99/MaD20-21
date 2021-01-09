using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Web.Pages.Product
{
    public partial class ProductsByLabelView : SpecificCulturePage
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


                IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

                IProductService productService = iocManager.Resolve<IProductService>();


                LoadCatalog(productService);
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

        protected void LoadCatalog(IProductService productService)
        {
            string dateFormat = GetFormat();
            ProductBlock productBlock;

            string labelName = Request.Params.Get("labelName");

            //Recuperamos la información del servicio
            productBlock = productService.ViewProductsByLabels(labelName, startIndex, count);

            GridViewCatalog.DataSource = productBlock.Products;

            GridViewCatalog.DataBind();

            lblLabelContent.Text = labelName;

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
                    "/Pages/Product/ProductsByLabelView.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&index=" + index + "&labelName=" + Request.Params.Get("labelName");

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/Product/ProductsByLabelView.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&index=" + index + "&labelName=" + Request.Params.Get("labelName");

                lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

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