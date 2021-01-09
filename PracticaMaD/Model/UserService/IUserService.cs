using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService
    {

        IUserDao UserDao { set; }


        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void ChangePassword(long id, String oldClearPassword,
            String newClearPassword);


        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserDetails FindUserDetails(long UserId);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginResult Login(String login, String password,
            Boolean passwordIsEncrypted);

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long SingUpUser(String login, String clearPassword,
            UserDetails userDetails);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserDetails(long id,
            UserDetails userDetails);

        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="DuplicateCreditCardException"/>
        [Transactional]
        CreditCard AddCreditCard(long userId, string ownerName, string creditType,
            string creditCardNumber, short cvv, DateTime expirationDate);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void SetCreditCardAsDefault(long userId, long creditCardId);

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        List<CreditCardDetails> FindCreditCardsByUserId(long userId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        String GetRolByUserId(long userId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        bool UserExists(string login);
    }
}
