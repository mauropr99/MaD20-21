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

        public Nullable<long> DefaultCreditCardId { get; private set; }

        #endregion

        public UserDetails(string name, string lastName,
            string email, string languageName, string languageCountry, long defaultCreditCardId)
        {
            this.Name = name;
            this.Lastname = lastName;
            this.Email = email;
            this.LanguageName = languageName;
            this.LanguageCountry = languageCountry;
            this.DefaultCreditCardId = defaultCreditCardId;
        }

        public UserDetails(string name, string lastName,
            string email, string languageName, string languageCountry)
        {
            this.Name = name;
            this.Lastname = lastName;
            this.Email = email;
            this.LanguageName = languageName;
            this.LanguageCountry = languageCountry;
            this.DefaultCreditCardId = null;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (this.Name == target.Name)
                  && (this.Lastname == target.Lastname)
                  && (this.Email == target.Email)
                  && (this.LanguageName == target.LanguageName)
                  && (this.LanguageCountry == target.LanguageCountry);
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
                "country = " + LanguageCountry + " ]";


            return strUserDetails;
        }
    }
}
