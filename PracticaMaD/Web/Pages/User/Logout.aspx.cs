using System;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class Logout : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionManager.Logout(Context);

            IShoppingService shoppingService = SessionManager.GetShoppingService();

            shoppingService.ClearShoppingCart();

            Response.Redirect("~/Pages/MainPage.aspx");



        }
    }
}
