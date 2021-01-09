using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao
{
    public interface IUserDao : IGenericDao<User, long>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="login">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLogin(string login);

        /// <exception cref="InstanceNotFoundException"></exception>
        User FindByEmail(string email);
    }
}

