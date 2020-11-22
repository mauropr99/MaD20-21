using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{

    [Serializable]
    public class DuplicatedCreditCardException : Exception
    {

        public DuplicatedCreditCardException(String creditCardNumber)
            : base("Duplicated credit card => credit card = " + "**** **** **** " + creditCardNumber.Substring(12))
        {
            this.CreditCardNumber = creditCardNumber;
        }

        public String CreditCardNumber { get; private set; }

        #region Test Code Region. Uncomment for testing.

        //public static void Main(String[] args)
        //{

        //    try
        //    {

        //        throw new IncorrectPasswordException("jsmith");

        //    }
        //    catch (Exception e)
        //    {

        //        LogManager.RecordMessage("Message: " + e.Message +
        //            "  Stack Trace: " + e.StackTrace, MessageType.Info);

        //        Console.ReadLine();

        //    }
        //}

        #endregion
    }
}
