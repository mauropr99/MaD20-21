using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors
{
    public partial class CreditCardAlreadyExpired : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string creditCard = Request.Params.Get("creditCard");
            lblErrorTitle.Text = GetLocalResourceObject("AlreadyExpired").ToString() + creditCard;
        }
    }
}