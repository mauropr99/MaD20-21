using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{

    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public IncorrectPasswordException(String login)
            : base("Incorrect password exception => loginName = " + login)
        {
            this.Login = login;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String Login { get; private set; }

    }
}
