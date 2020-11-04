using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao
{
    /// <summary>
    /// Specific Operations for UserProfile
    /// </summary>
    public class UserDaoEntityFramework :
        GenericDaoEntityFramework<User_Table, Int64>, IUserDao
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
        public User_Table FindByLogin(string login)
        {
            User_Table userProfile = null;

            #region Option 1: Using Linq.

            DbSet<User_Table> user = Context.Set<User_Table>();

            var result =
                (from u in user
                 where u.login == login
                 select u);

            user = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            
            if (user == null)
                throw new InstanceNotFoundException(login,
                    typeof(User_Table).FullName);

            return userProfile;
        }

        #endregion IUserProfileDao Members
    }

}
