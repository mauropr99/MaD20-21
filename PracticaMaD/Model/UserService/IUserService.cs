﻿using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService
    {

        IUserDao UserDao { set; }

        [Transactional]
        void ChangePassword(long id, String oldClearPassword,
            String newClearPassword);

        [Transactional]
        UserDetails FindUserDetails(long UserId);

        [Transactional]
        LoginResult Login(String login, String password,
            Boolean passwordIsEncrypted);

        [Transactional]
        long SingUpUser(String login, String clearPassword,
            UserDetails userDetails);

        [Transactional]
        void UpdateUserDetails(long id,
            UserDetails userDetails);

        [Transactional]
        CreditCard AddCreditCard(long userId, string ownerName, string creditType,
            string creditCardNumber, short cvv, DateTime expirationDate);

        [Transactional]
        void SetCreditCardAsDefault(long userId, long creditCardId);

        [Transactional]
        List<CreditCardDetails> FindCreditCardsByUserId(long userId);

        [Transactional]
        String GetRolByUserId(long userId);

        [Transactional]
        bool UserExists(string login);
    }
}
