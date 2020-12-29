using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class CreditCardDetails
    {
        #region Properties Region

        public long CreditCardId { get; private set; }

        public string OwnerName { get; private set; }

        public string CreditCardType { get; private set; }

        public string AnonymizedCreditCardNumber { get; private set; }

        public string ExpirationDate { get; private set; }

        public CreditCardDetails(long creditCardId, string ownerName, string creditCardType, string creditCardNumber, DateTime expirationDate)
        {
            CreditCardId = creditCardId;
            OwnerName = ownerName;
            CreditCardType = creditCardType;
            AnonymizedCreditCardNumber = "**** **** **** " + creditCardNumber.Substring(12);
            ExpirationDate = expirationDate.ToString("MM/dd/yyyy");
        }

        #endregion Properties Region


        public static List<CreditCardDetails> fromCreditCardToCreditCardDetails(List<CreditCard> creditCards)
        {
            List<CreditCardDetails> creditCardDetailsList = new List<CreditCardDetails>();
            foreach (var creditCard in creditCards)
            {
                CreditCardDetails newCreditCardDetails = new CreditCardDetails(creditCard.id, 
                    creditCard.ownerName, creditCard.creditType, creditCard.creditCardNumber, creditCard.expirationDate);

                creditCardDetailsList.Add(newCreditCardDetails);
            }

            return creditCardDetailsList;
        }

       
    }
}
