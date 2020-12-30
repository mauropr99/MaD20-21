using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public ILanguageDao LanguageDao { private get; set; }
        [Inject]
        public ICreditCardDao CreditCardDao { private get; set; }

        #region IUserService Members

        [Transactional]
        public void ChangePassword(long id, string oldClearPassword,
            string newClearPassword)
        {
            User user = UserDao.Find(id);
            String storedPassword = user.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(user.login);
            }

            user.password =
            PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserDetails FindUserDetails(long id)
        {
            User user = UserDao.Find(id);
            Language language = LanguageDao.FindByUserId(user.id);
            UserDetails userDetails;

            if (user.favouriteCreditCard == null)
            {
                userDetails = 
                new UserDetails(user.name,
                    user.lastName, user.email,
                    language.name, language.country);
            } else
            {
                userDetails =
                new UserDetails(user.name,
                    user.lastName, user.email,
                    language.name, language.country, user.favouriteCreditCard.Value);
            }
            

            return userDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(String login, string password, bool passwordIsEncrypted)
        {
            User user =
                UserDao.FindByLogin(login);

            string storedPassword = user.password;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }

            Language language = LanguageDao.FindByUserId(user.id);

            return new LoginResult(user.id,user.login, user.name,user.lastName,
                storedPassword, language.name, language.country, user.email);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long SingUpUser(string login, string clearPassword, UserDetails userDetails)
        {
            try
            {
                UserDao.FindByLogin(login);

                throw new DuplicateInstanceException(login,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                string encryptedPassword = PasswordEncrypter.Crypt(clearPassword);
                Language language = new Language();
                try
                {
                    language = LanguageDao.FindByNameAndCountry(userDetails.LanguageName, userDetails.LanguageCountry);
                } catch (InstanceNotFoundException)
                {
                    //Take browser default language
                }

                User user = new User
                {
                    login = login,
                    password = encryptedPassword,
                    name = userDetails.Name,
                    lastName = userDetails.Lastname,
                    email = userDetails.Email,
                    Language = language,
                    role = "user"
                };

                UserDao.Create(user);

                return user.id;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        [Transactional]
        public void UpdateUserDetails(long id, UserDetails userDetails)
        {
            User user = UserDao.Find(id);

            user.name = userDetails.Name;
            user.lastName = userDetails.Lastname;
            user.email = userDetails.Email;
            user.languageId = LanguageDao.FindByNameAndCountry(userDetails.LanguageName, userDetails.LanguageCountry).id;
        }

        /// <exception cref="DuplicateCreditCardException"/>
        [Transactional]
        public CreditCard AddCreditCard(long userId, string ownerName, string creditType,
            string creditCardNumber, short cvv, DateTime expirationDate)
        {

            User user = UserDao.Find(userId);

            List<CreditCard> creditCards = CreditCardDao.FindCreditCardsByUserId(userId);

           


            foreach (CreditCard creditCard in creditCards)
            {
                if (creditCard.creditCardNumber == creditCardNumber)
                {
                    throw new DuplicatedCreditCardException(creditCardNumber);
                }
            }

            CreditCard newCreditCard = new CreditCard
            {
                ownerName = ownerName,
                creditType = creditType,
                creditCardNumber = creditCardNumber,
                cvv = cvv,
                expirationDate = expirationDate
            };
            CreditCardDao.Create(newCreditCard);

            //If this is the only creditCard, it'll be the default card
            if (creditCards.Count() == 0)
            {
                user.favouriteCreditCard = newCreditCard.id;
                UserDao.Update(user);
            }

            CreditCardDao.AddUser(user, newCreditCard.id);

            return newCreditCard;
        }

        public void SetCreditCardAsDefault(long userId, long creditCardId)
        {

            User user = UserDao.Find(userId);

            user.favouriteCreditCard = creditCardId;

            UserDao.Update(user);
           
        }

        public bool UserExists(string login)
        {
            try
            {
                User user = UserDao.FindByLogin(login);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

        public List<CreditCardDetails> FindCreditCardsByUserId(long userId)
        {
            User user = UserDao.Find(userId);
            List<CreditCard> creditCards = CreditCardDao.FindCreditCardsByUserId(userId);

            return CreditCardDetails.fromCreditCardToCreditCardDetails(creditCards);
        }


        #endregion IUserService Members
    }
}