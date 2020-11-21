using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
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
        private const string role = "user";

        private const long NON_EXISTENT_USER_ID = -1;

        private static IKernel kernel;
        private static IUserService userService;


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

            TestUtil.userDao = kernel.Get<IUserDao>();
            TestUtil.languageDao = kernel.Get<ILanguageDao>();
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();

            userService = kernel.Get<IUserService>();

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
                Language language = TestUtil.CreateExistentLanguage();

                var id =
                    userService.SingUpUser(login, password,
                        new UserDetails(name, lastName, email, language.name, address));

                var user = TestUtil.userDao.Find(id);

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
        public void SingUpDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

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
                Language language = TestUtil.CreateExistentLanguage();

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

        [TestMethod()]
        public void AddCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();
                User user = TestUtil.CreateExistentUser(language);

                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber = "1234567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                CreditCard createdCreditCard = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

                List<CreditCard> creditCards = TestUtil.creditCardDao.FindCreditCardsByUserLogin(user.login);

                Assert.IsTrue(creditCards.Contains(createdCreditCard));

            }
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicatedCreditCardException))]
        public void AddDuplicatedCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();
                User user = TestUtil.CreateExistentUser(language);

                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber = "1234567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

                userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

            }
        }
    }
}