using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class CreditCardAlreadyExpired : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CreditCardAlreadyExpired"/> class.
        /// </summary>
        /// <param name="creditCardNumber"><c>number of credit card</c> that causes the error.</param>
        public CreditCardAlreadyExpired(String creditCardNumber)
            : base("CreditCard Already Expired => CreditCardNumber = " + creditCardNumber)
        {
            this.CreditCardNumber = "**** **** **** " + creditCardNumber.Substring(12); ;
        }

        public String CreditCardNumber { get; private set; }
    }
}
