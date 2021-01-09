using System;
using System.Collections.Generic;
using System.Transactions;
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
        private static Language language;
        private static User user;



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

            language = TestUtil.CreateExistentLanguage();
            user = TestUtil.CreateExistentUser(language);

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

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() { 
            transactionScope.Dispose();
        }


        #endregion Additional test attributes

        [TestMethod()]
        public void SignUpUserTest()
        {

            using (var scope = new TransactionScope())
            {
                string login = "user2";
                var id =
                    userService.SingUpUser(login, password,
                        new UserDetails(name, lastName, email, language.name, language.country));

                var user = TestUtil.userDao.Find(id);

                Assert.AreEqual(id, user.id);
                Assert.AreEqual(login, user.login);
                Assert.AreEqual(name, user.name);
                Assert.AreEqual(lastName, user.lastName);
                Assert.AreEqual(email, user.email);
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

                userService.SingUpUser(login, password,
                    new UserDetails(name, lastName, email, language.name, language.country));

                userService.SingUpUser(login, password,
                        new UserDetails(name, lastName, email, language.name, language.country));
            }
        }

        [TestMethod()]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                string login = "user2";
                var id = userService.SingUpUser(login, password,
                         new UserDetails(name, lastName, email, language.name, language.country));

                var expected = new LoginResult(id, login, name, lastName,
                   PasswordEncrypter.Crypt(password), language.name, language.country, email);

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
                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber = "1234567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                CreditCard createdCreditCard = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

                List<CreditCard> creditCards = TestUtil.creditCardDao.FindCreditCardsByUserId(user.id);

                Assert.IsTrue(creditCards.Contains(createdCreditCard));

            }
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicatedCreditCardException))]
        public void AddDuplicatedCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                
                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber = "1234567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

                userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber, cvv, expirationDate);

            }
        }

        [TestMethod()]
        public void SetCreditCardAsDefaultTest()
        {
            using (var scope = new TransactionScope())
            {
                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber1 = "1234567891234567";
                string creditCardNumber2 = "1114567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                CreditCard creditCard1 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber1, cvv, expirationDate);
                User foundUser = TestUtil.userDao.Find(user.id);
                Assert.AreEqual(creditCard1.id, foundUser.favouriteCreditCard);


                CreditCard creditCard2 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber2, cvv, expirationDate);
                userService.SetCreditCardAsDefault(user.id, creditCard2.id);
                User foundUser2 = TestUtil.userDao.Find(user.id);
                Assert.AreEqual(creditCard2.id, foundUser2.favouriteCreditCard);



            }
        }

        [TestMethod()]
        public void FindCreditCardsByUserId()
        {
            using (var scope = new TransactionScope())
            {
                string ownerName = "Name Surname";
                string creditType = "debit";
                string creditCardNumber1 = "1234567891234567";
                string creditCardNumber2 = "2234567891234567";
                string creditCardNumber3 = "3234567891234567";
                string creditCardNumber4 = "4234567891234567";
                short cvv = 123;
                DateTime expirationDate = DateTime.Now.AddYears(1);

                CreditCard creditCard1 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber1, cvv, expirationDate);
                Assert.AreEqual(1, userService.FindCreditCardsByUserId(user.id).Count);
                Assert.AreEqual(creditCard1.id, userService.FindCreditCardsByUserId(user.id)[0].CreditCardId);

                CreditCard creditCard2 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber2, cvv, expirationDate);
                Assert.AreEqual(2, userService.FindCreditCardsByUserId(user.id).Count);
                Assert.AreEqual(creditCard2.id, userService.FindCreditCardsByUserId(user.id)[1].CreditCardId);

                CreditCard creditCard3 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber3, cvv, expirationDate);
                Assert.AreEqual(3, userService.FindCreditCardsByUserId(user.id).Count);
                Assert.AreEqual(creditCard3.id, userService.FindCreditCardsByUserId(user.id)[2].CreditCardId);

                CreditCard creditCard4 = userService.AddCreditCard(user.id, ownerName, creditType, creditCardNumber4, cvv, expirationDate);
                Assert.AreEqual(4, userService.FindCreditCardsByUserId(user.id).Count);
                Assert.AreEqual(creditCard4.id, userService.FindCreditCardsByUserId(user.id)[3].CreditCardId);

            }
        }

    }
}