﻿using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class DuplicatedCreditCardException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DuplicatedCreditCardException"/> class.
        /// </summary>
        /// <param name="creditCardNumber"><c>number of credit card</c> that causes the error.</param>
        public DuplicatedCreditCardException(string creditCardNumber)
            : base("Duplicated credit card => credit card = " + "**** **** **** " + creditCardNumber.Substring(12))
        {
            CreditCardNumber = creditCardNumber;
        }

        public string CreditCardNumber { get; private set; }

    }
}
