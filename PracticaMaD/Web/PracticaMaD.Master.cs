using System;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;



namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (HyperLinkUpdate != null)
                    HyperLinkUpdate.Visible = false;
                if (HyperLinkLogout != null)
                    HyperLinkLogout.Visible = false;
                if (HyperLinkViewCreditCards != null)
                    HyperLinkViewCreditCards.Visible = false;
                if (HyperLinkOrderHistory != null)
                    HyperLinkOrderHistory.Visible = false;

            }
            else
            {
                if (HyperLinkAuth != null)
                    HyperLinkAuth.Visible = false;

            }

            IShoppingService shoppingService = SessionManager.GetShoppingService();
            ShoppingCartSize.Text = "(" + shoppingService.ViewShoppingCart().Count.ToString() + ")";

        }
    }
}
