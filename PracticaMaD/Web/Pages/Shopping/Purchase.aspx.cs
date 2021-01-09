using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Purchase : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            LoadDropDownCreditCardsList(userService);
        }

        protected void LoadDropDownCreditCardsList(IUserService userService)
        {
            int index = 0, defaultCreditCardIndex = 0;

            if (DropDownCreditCardsList.Items.Count == 0)
            {
                UserSession userSession =
                    (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

                if (userSession == null) Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                List<CreditCardDetails> creditCards = userService.FindCreditCardsByUserId(userSession.UserId);
                UserDetails user = userService.FindUserDetails(userSession.UserId);

                DropDownCreditCardsList.Items.Clear();

                if (creditCards.Count == 0)
                {
                    DropDownCreditCardsList.Items.Add("--Select--");
                }
                else
                {
                    foreach (CreditCardDetails creditCard in creditCards)
                    {
                        if (user.DefaultCreditCardId == creditCard.CreditCardId) defaultCreditCardIndex = index;
                        DropDownCreditCardsList.Items.Add(creditCard.AnonymizedCreditCardNumber);
                        DropDownCreditCardsList.Items[index].Value = creditCard.CreditCardId.ToString();
                        index++;
                    }
                    DropDownCreditCardsList.SelectedIndex = defaultCreditCardIndex;

                }
            }
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
                                            long.Parse(DropDownCreditCardsList.SelectedValue), txtDeliveryDescription.Text);

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/Catalog.aspx"));
            }
            catch (CreditCardAlreadyExpired a)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/CreditCardAlreadyExpired.aspx?creditCard=" + a.CreditCardNumber));
            }
            catch (NotEnoughStock b)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/OutOfStock.aspx?productName=" + b.ProductName + "&stock=" + b.Stock.ToString() + "&OrderedStock=" + b.LineQuantity.ToString()));
            }
            catch (DifferentPrice)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }

        }
    }
}