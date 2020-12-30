using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Web.Pages.Shopping
{
    public partial class OrderHistory : SpecificCulturePage
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

            this.GridOrderHistory.DataSource = orderBlock.Orders;

            this.GridOrderHistory.DataBind();

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
                String url =
                    "/Pages/Shopping/OrderHistory.aspx" + "?startIndex=" + (startIndex + count) + "&count=" +
                    count + "&index=" + index;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/Shopping/OrderHistory.aspx" + "?startIndex=" + (startIndex - count) + "&count=" +
                    count + "&index=" + index;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

        }
    }
}