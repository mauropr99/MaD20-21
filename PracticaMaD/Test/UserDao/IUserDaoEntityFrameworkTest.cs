using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao.Test
{
    [TestClass()]
    public class IUserDaoEntityFrameworkTest
    {

        private static IKernel kernel;
        private static IUserDao userDao;
        private static ILanguageDao languageDao;

        private const string NON_EXISTENT_USER = "no_user";

        private static Language createExistentLanguage(Language language)
        {
            language.name = "español";
            language.country = "España";
            languageDao.Create(language);
            
            return language;
        }

        private static User createExistentUser(User user, Language language)
        {
            
            user.login = "user";
            user.name = "usuario";
            user.lastName = "dePrueba";
            user.password = "passwd";
            user.address = "A Coruña";
            user.email = "user@user";
            user.role = "user";
            user.languageId = language.id;
            userDao.Create(user);
            
            return user;
        }

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userDao = kernel.Get<IUserDao>();
            languageDao = kernel.Get<ILanguageDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void FindByLoginTest()
        {
            Language language = new Language();
            createExistentLanguage(language);
            User user = new User();
            createExistentUser(user, language);
            User foundUser = new User();
            foundUser = userDao.FindByLogin("user");

            Assert.AreEqual(user.id, foundUser.id);
            Assert.AreEqual(user.login, foundUser.login);
            Assert.AreEqual(user.name, foundUser.name);
            Assert.AreEqual(user.lastName, foundUser.lastName);
            Assert.AreEqual(user.password, foundUser.password);
            Assert.AreEqual(user.address, foundUser.address);
            Assert.AreEqual(user.email, foundUser.email);
            Assert.AreEqual(user.languageId, foundUser.languageId);
            Assert.AreEqual(user.role, foundUser.role);
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLoginTest()
        {
            User foundUser = userDao.FindByLogin(NON_EXISTENT_USER);
        }
    }
}


