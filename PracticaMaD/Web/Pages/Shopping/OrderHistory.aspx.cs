using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Web.Pages.Shopping
{
    public partial class OrderHistory : SpecificCulturePage
    {
        int startIndex = 0, index = 0;
        int count = 4;

        protected void Page_Load(object sender, EventArgs e)
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
        protected void LoadPage()
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            LoadOrderHistory(shoppingService);
        }

        protected void LoadOrderHistory(IShoppingService shoppingService)
        {
            string dateFormat = "MM/dd/yyyy";
            OrderBlock orderBlock;

            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            orderBlock = shoppingService.FindOrdersByUserId(userSession.UserId, startIndex, count);

            GridOrderHistory.DataSource = orderBlock.Orders;

            GridOrderHistory.DataBind();

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

            for (int i = 0; i < GridOrderHistory.Rows.Count; i++)
            {
                GridOrderHistory.Rows[i].Cells[1].Text = orderBlock.Orders[i].Date.ToString(dateFormat);
                GridOrderHistory.Rows[i].Cells[2].Text = orderBlock.Orders[i].TotalPrice.ToString("C2");
            }


            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            if (orderBlock.ExistMoreOrders)
            {
                string url =
                    "/Pages/Shopping/OrderHistory.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&index=" + index;

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/Shopping/OrderHistory.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&index=" + index;

                lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

        }
    }
}