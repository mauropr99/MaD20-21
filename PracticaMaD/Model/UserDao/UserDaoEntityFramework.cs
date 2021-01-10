using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao
{
    /// <summary>
    /// Specific Operations for UserProfile
    /// </summary>
    public class UserDaoEntityFramework :
        GenericDaoEntityFramework<User, long>, IUserDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by his loginName
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByLogin(string login)
        {
            User user = null;

            #region Option 1: Using Linq.

            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.login == login
                 select u);

            user = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (user == null)
                throw new InstanceNotFoundException(login,
                    typeof(User).FullName);

            return user;
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByEmail(string email)
        {
            User user = null;

            #region Option 1: Using Linq.

            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.email == email
                 select u);

            user = result.FirstOrDefault();

            #endregion Option 1: Using Linq.


            if (user == null)
                throw new InstanceNotFoundException(email,
                    typeof(User).FullName);

            return user;
        }

        #endregion IUserDao Members
    }

}
