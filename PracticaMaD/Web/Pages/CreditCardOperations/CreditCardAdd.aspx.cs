﻿using System;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations
{
    public partial class CreditCardAdd : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void BtnAddCreditCard_Click(object sender, EventArgs e)
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


            DateTime expirationDate = DateTime.ParseExact(expirationDateString, "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            productService.AddCreditCard(userSession.UserId, creditCardOwner, creditCardType, creditCardNumber, short.Parse(creditCardCvv), expirationDate);

            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }

    }
}