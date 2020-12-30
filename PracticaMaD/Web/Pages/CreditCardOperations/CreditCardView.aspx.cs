using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations
{
    public partial class CreditCardView : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string format = "MM/dd/yyyy";
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



            //Changing the date format...
            Locale locale = SessionManager.GetLocale(Context);

            switch (locale.Country)
            {
                case "ES":
                    format = "dd/MM/yyyy";
                    break;
                case "US":
                    format = "MM/dd/yyyy";
                    break;

                default:
                    format = "MM/dd/yyyy";
                    break;
            }

            for (int i = 0; i < GridViewCreditCards.Rows.Count; i++)
            {
                GridViewCreditCards.Rows[i].Cells[3].Text = creditCards[i].ExpirationDate.ToString(format);
            }

        }

        protected void GridViewCreditCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void BtnAddNewCreditCard_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/CreditCardOperations/CreditCardAdd.aspx"));
        }
    }
}