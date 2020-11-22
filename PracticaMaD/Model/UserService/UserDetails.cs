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

        public string LanguageName { get; private set; }

        public string LanguageCountry { get; private set; }

        public string Address { get; private set; }

        #endregion

        public UserDetails(string name, string lastName,
            string email, string languageName, string languageCountry, string address)
        {
            this.Name = name;
            this.Lastname = lastName;
            this.Email = email;
            this.LanguageName = languageName;
            this.LanguageCountry = languageCountry;
            this.Address = address;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (this.Name == target.Name)
                  && (this.Lastname == target.Lastname)
                  && (this.Email == target.Email)
                  && (this.LanguageName == target.LanguageName)
                  && (this.LanguageCountry == target.LanguageCountry)
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
                "language = " + LanguageName + " | " +
                "country = " + LanguageCountry + " | " +
                "address = " + Address + " ]";


            return strUserDetails;
        }
    }
}
