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

        [Transactional]
        LoginResult Login(System.String loginName, System.String password,
            Boolean passwordIsEncrypted);

        [Transactional]
        long SingUpUser(System.String loginName, System.String clearPassword,
            UserDetails userDetails);
    }
}
