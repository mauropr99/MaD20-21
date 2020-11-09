using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }

        #region IUserService Members

        [Transactional]
        public void ChangePassword(long id, string oldClearPassword,
            string newClearPassword)
        {
            User_Table user = UserDao.Find(id);
            System.String storedPassword = user.password;

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
            User_Table user = UserDao.Find(id);

            UserDetails userDetails =
                new UserDetails(user.name,
                    user.lastName, user.email,
                    user.Language.name, user.address);

            return userDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(String login, string password, bool passwordIsEncrypted)
        {
            User_Table user =
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

            return new LoginResult(user.id,user.login, user.name,user.lastName,
                storedPassword, user.Language.name, user.email,user.address);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long SingUpUser(string login, string clearPassword, UserDetails userDetails)
        {
            try
            {
                UserDao.FindByLogin(login);

                throw new DuplicateInstanceException(login,
                    typeof(User_Table).FullName);
            }
            catch (InstanceNotFoundException)
            {
                string encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                User_Table user = new User_Table();

                user.login = login;
                user.password = encryptedPassword;
                user.name = userDetails.Name;
                user.lastName = userDetails.Lastname;
                user.email = userDetails.Email;
                user.Language.name = userDetails.Language;
                user.address = userDetails.Address;
                user.role = "user";

                UserDao.Create(user);

                return user.id;
            }
        }

        public bool UserExists(string login)
        {

            try
            {
                User_Table userProfile = UserDao.FindByLogin(login);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

        #endregion IUserService Members
    }
}