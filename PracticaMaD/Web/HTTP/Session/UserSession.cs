using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {

        private long userId;
        private String firstName;

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
    }
}