using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    [Serializable]
    public class CreditCardAlreadyExpired : Exception
    {
        public CreditCardAlreadyExpired(String creditCardNumber)
            : base("CreditCard Already Expired => CreditCardNumber = " + creditCardNumber)
        {
            this.CreditCardNumber = "**** **** **** " + creditCardNumber.Substring(12); ;
        }

        public String CreditCardNumber { get; private set; }
    }
}
