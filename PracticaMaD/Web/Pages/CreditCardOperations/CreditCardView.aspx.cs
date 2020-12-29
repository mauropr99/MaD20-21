using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations
{
    public partial class CreditCardView : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            //2 Obtener el servicio

            IUserService userService = iocManager.Resolve<IUserService>();

            //Llamar a los casos de uso
            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            List<CreditCardDetails> creditCards = userService.FindCreditCardsByUserId(userSession.UserId);


            this.GridViewCreditCards.DataSource = creditCards;

            this.GridViewCreditCards.DataBind();

        }

        protected void GridViewCreditCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnAddNewCreditCard_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/CreditCardOperations/CreditCardAdd.aspx"));
        }
    }
}