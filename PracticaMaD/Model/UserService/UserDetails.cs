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
            Name = name;
            Lastname = lastName;
            Email = email;
            LanguageName = languageName;
            LanguageCountry = languageCountry;
            DefaultCreditCardId = defaultCreditCardId;
        }

        public UserDetails(string name, string lastName,
            string email, string languageName, string languageCountry)
        {
            Name = name;
            Lastname = lastName;
            Email = email;
            LanguageName = languageName;
            LanguageCountry = languageCountry;
            DefaultCreditCardId = null;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (Name == target.Name)
                  && (Lastname == target.Lastname)
                  && (Email == target.Email)
                  && (LanguageName == target.LanguageName)
                  && (LanguageCountry == target.LanguageCountry);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
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
