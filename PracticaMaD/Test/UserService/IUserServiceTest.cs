using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Test
{
    [TestClass()]
    public class IUserServiceTest
    {
        private const string login = "user";
        private const string name = "name";
        private const string lastName = "lastName";
        private const string password = "passwd";
        private const string email = "user@udc.es";
        private const string address = "A Coruña";
        private const string languageName = "es";
        private const string languageCountry = "ES";
        private const string role = "user";
        private static Language language = new Language();

        private const long NON_EXISTENT_USER_ID = -1;

        private static IKernel kernel;
        private static IUserService userService;
        private static IUserDao userDao;


        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();

            userService = kernel.Get<IUserService>();

            language.name = languageName;
            language.country = languageCountry;

    }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void SignUpUserTest()
        {
            using (var scope = new TransactionScope())
            { 
                var id =
                    userService.SingUpUser(login, password,
                        new UserDetails(name, lastName, email, language.name, address));

                var user = userDao.Find(id);

                Assert.AreEqual(id, user.id);
                Assert.AreEqual(login, user.login);
                Assert.AreEqual(name, user.name);
                Assert.AreEqual(lastName, user.lastName);
                Assert.AreEqual(email, user.email);
                Assert.AreEqual(address, user.address);
                Assert.AreEqual(language.name, user.Language.name);
                Assert.AreEqual(role, user.role);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.SingUpUser(login, password,
                         new UserDetails(name, lastName, email, language.name, address));

                userService.SingUpUser(login, password,
                        new UserDetails(name, lastName, email, language.name, address));
            }
        }

        [TestMethod()]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                var id = userService.SingUpUser(login, password,
                         new UserDetails(name, lastName, email, language.name, address));

                var expected = new LoginResult(id, login, name, lastName,
                   PasswordEncrypter.Crypt(password), language.name, email, address);

                var actual =
                    userService.Login(login,
                        password, false);

                Assert.AreEqual(expected, actual);
            }
        }
    }
}