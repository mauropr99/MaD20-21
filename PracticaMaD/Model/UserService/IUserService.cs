using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService
    {
 
        IUserDao UserDao { set; }

        void ChangePassword(long id, String oldClearPassword,
            String newClearPassword);

        [Transactional]
        UserDetails FindUserDetails(long userProfileId);

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

        bool UserExists(string login);
    }
}
