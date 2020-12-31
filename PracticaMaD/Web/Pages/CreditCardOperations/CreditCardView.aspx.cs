using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using System.Web.UI.WebControls;
using System.Globalization;

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

            UserDetails userDetails = userService.FindUserDetails(userSession.UserId);

            //Cargamos la tarjeta por defecto (en caso de existir)
            foreach (var creditCard in creditCards)
            {
                if (creditCard.CreditCardId == userDetails.DefaultCreditCardId)
                {
                    creditCard.IsDefaultCreditCard = true;
                }
            }

            GridViewCreditCards.DataSource = creditCards;

            GridViewCreditCards.DataBind();


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
                if(GridViewCreditCards.DataKeys[i].Values[1].ToString() == "True")
                {
                    GridViewCreditCards.Rows[i].Cells[4].Visible = false;
                }
                GridViewCreditCards.Rows[i].Cells[3].Text = creditCards[i].ExpirationDate.ToString(format);
            }

        }

        protected void btnAddNewCreditCard_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/CreditCardOperations/CreditCardAdd.aspx"));
        }

        protected void GridViewCreditCards_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetAsDefault")
            {
                
                //1 Obtener contexto de inyección de dependencias

                IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

                //2 Obtener el servicio

                IUserService userService = iocManager.Resolve<IUserService>();

                //Llamar a los casos de uso
                UserSession userSession =
                    (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];
                

                int index = Convert.ToInt32(e.CommandArgument);

                long creditCardId = long.Parse(GridViewCreditCards.DataKeys[index].Values[0].ToString());


                userService.SetCreditCardAsDefault(userSession.UserId, creditCardId);

                /*
                 Volvemos a poner todos los botones visibles e invisibilizamos aquel que corresponda
                 al índice de la tarjeta que hemos seleccionado como tarjeta por defecto.
                 */
                for (int i = 0; i < GridViewCreditCards.Rows.Count; i++)
                    GridViewCreditCards.Rows[i].Cells[4].Visible = true;

                GridViewCreditCards.Rows[index].Cells[4].Visible = false;

            }


        }
    }
}