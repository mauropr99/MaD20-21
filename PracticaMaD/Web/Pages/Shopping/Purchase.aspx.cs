using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Purchase : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void UpdateCreditCards(String selectedCreditCard)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];
            
            this.creditCards.DataSource = userService.FindCreditCardsByUserId(userSession.UserId);
        }

        protected void BtnPurchaseClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
               
                //Crear dto para compra

            }
        }

        protected void CreditCardsSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
             * correct language.
             */
            this.UpdateCreditCards(creditCards.SelectedValue);
        }
    }
}