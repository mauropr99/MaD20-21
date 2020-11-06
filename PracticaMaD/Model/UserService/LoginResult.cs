using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    [Serializable()]
    public class LoginResult
    {

            public LoginResult(long id, string login, string name,
                string lastName, string encryptedPassword, String language,
                string email, string address)
            {
                this.Id = id;
                this.Login = login;
                this.Name = name;
                this.LastName = lastName;
                this.EncryptedPassword = encryptedPassword;
                this.Language = language;
                this.Email = email;
                this.Address = Address;
            }

            #region Properties Region

            public string Email { get; private set; }

            public string EncryptedPassword { get; private set; }

            public string Address { get; private set; }

            public string Name { get; private set; }

            public string LastName { get; private set; }

            public String Language { get; private set; }

            public string Login { get; private set; }

            public long Id { get; private set; }

            #endregion Properties Region

            public override bool Equals(object obj)
            {
                LoginResult target = (LoginResult)obj;

                return (this.Id == target.Id)
                       && (this.Name == target.Name)
                       && (this.LastName == target.LastName)
                       && (this.Address == target.Address)
                       && (this.Login == target.Login)
                       && (this.EncryptedPassword == target.EncryptedPassword)
                       && (this.Language == target.Language)
                       && (this.Email == target.Email);
            }


            public override int GetHashCode()
            {
                return this.Id.GetHashCode();
            }

            public override string ToString()
            {
            string strLoginResult;

                strLoginResult =
                    "[ userId = " + Id + " | " +
                    "Name = " + Name + " | " +
                    "LastName = " + LastName + " | " +
                    "encryptedPassword = " + EncryptedPassword + " | " +
                    "language = " + Language + " | " +
                    "login = " + Login + " | " +
                    "Adress = " + Address + " | " +
                    "country = " + Email + " ]";

                return strLoginResult;
            }

    }
}