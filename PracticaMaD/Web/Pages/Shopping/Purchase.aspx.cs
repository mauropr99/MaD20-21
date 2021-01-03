using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Purchase : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e) { 

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            LoadDropDownCreditCardsList(userService);
        }

        protected void LoadDropDownCreditCardsList(IUserService userService)
        {
            int index = 0;

            if (DropDownCreditCardsList.Items.Count == 0)
            {
                UserSession userSession =
                    (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

                List<CreditCardDetails> creditCards = userService.FindCreditCardsByUserId(userSession.UserId);

                this.DropDownCreditCardsList.Items.Clear();

                foreach (CreditCardDetails creditCard in creditCards)
                {
                    this.DropDownCreditCardsList.Items.Add(creditCard.AnonymizedCreditCardNumber);
                    this.DropDownCreditCardsList.Items[index].Value = creditCard.CreditCardId.ToString();
                    index++;
                }
            }

            this.DropDownCreditCardsList.Visible = true;
        }

        protected void BtnPurchaseClick(object sender, EventArgs e)
        {
            IShoppingService shoppingService = SessionManager.GetShoppingService();
            List<ShoppingCartDetails> shoppingCart = shoppingService.ViewShoppingCart();
            
            UserSession userSession =
               (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            try
            {
                shoppingService.BuyProducts(userSession.UserId, shoppingCart, txtPostalAddress.Text,
                                            Int64.Parse(DropDownCreditCardsList.SelectedValue), txtDeliveryDescription.Text);

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/Catalog.aspx"));
            }
            catch
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }
        }   
    }
}