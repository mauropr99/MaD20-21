using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Globalization;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations
{
    public partial class CreditCardAdd : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAddCreditCard_Click(object sender, EventArgs e)
        {
            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            //2 Obtener el servicio

            IUserService productService = iocManager.Resolve<IUserService>();


            //3 Llamar al caso de uso (lectura de parámetros y actualización de la vista)

            String creditCardOwner = txtCreditCardOwner.Text;
            String creditCardNumber = txtCreditCardNumber.Text;
            String creditCardCvv = txtCreditCardCvv.Text;
            String creditCardType = DropDownCreditCardTypeList.SelectedValue;
            String expirationDateString = txtExpirationDate.Text;

            var cultureInfo = new CultureInfo("en-US");
            DateTime expirationDate = DateTime.Parse(expirationDateString, cultureInfo);


            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            productService.AddCreditCard(userSession.UserId, creditCardOwner, creditCardType, creditCardNumber, short.Parse(creditCardCvv), expirationDate);

            Response.Redirect("~/Pages/CreditCardOperations/CreditCardView.aspx");
        }

    }
}