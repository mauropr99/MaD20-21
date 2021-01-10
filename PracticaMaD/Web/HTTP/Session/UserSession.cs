namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {

        private long userId;
        private string firstName;

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
    }
}