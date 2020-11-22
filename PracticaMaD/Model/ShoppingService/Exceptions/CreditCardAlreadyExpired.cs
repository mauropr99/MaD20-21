using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    [Serializable]
    public class CreditCardAlreadyExpired : Exception
    {
        public CreditCardAlreadyExpired(String CreditCardNumber)
            : base("CreditCard Already Expired => CreditCardNumber = " + CreditCardNumber)
        {
            this.CreditCardNumber = CreditCardNumber;
        }

        public String CreditCardNumber { get; private set; }
    }
}
