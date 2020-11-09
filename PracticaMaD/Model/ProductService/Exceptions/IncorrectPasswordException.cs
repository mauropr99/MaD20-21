using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService.Exceptions
{
    [Serializable]
    public class IncorrectPasswordException : Exception
    {

        public IncorrectPasswordException(String login)
            : base("Incorrect password exception => loginName = " + login)
        {
            this.Login = login;
        }

        public String Login { get; private set; }

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

