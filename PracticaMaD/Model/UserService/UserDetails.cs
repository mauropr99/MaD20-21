using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{

    [Serializable()]
    public class UserDetails
    {
        #region Properties Region

        public string Name { get; private set; }

        public string Lastname { get; private set; }

        public string Email { get; private set; }

        public string Language { get; private set; }

        public string Address { get; private set; }

        #endregion

        public UserDetails(string name, string lastName,
            string email, string language, string address)
        {
            this.Name = name;
            this.Lastname = lastName;
            this.Email = email;
            this.Language = language;
            this.Address = address;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (this.Name == target.Name)
                  && (this.Lastname == target.Lastname)
                  && (this.Email == target.Email)
                  && (this.Language == target.Language)
                  && (this.Address == target.Address);
        }
    
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            string strUserDetails;

            strUserDetails =
                "[ firstName = " + Name + " | " +
                "lastName = " + Lastname + " | " +
                "email = " + Email + " | " +
                "language = " + Language + " | " +
                "country = " + Address + " ]";


            return strUserDetails;
        }
    }
}
