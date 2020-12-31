using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao.Tests
{
    [TestClass()]
    public class ICreditCardDaoEntityFrameworkTests
    {
        private static IKernel kernel;

        // Variables used in several tests are initialized here
        private const long userId = 123456;
        private const long NON_EXISTENT_USER_ID = -2;

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
            TestUtil.userDao = kernel.Get<IUserDao>();
            TestUtil.languageDao = kernel.Get<ILanguageDao>();
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();
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
        public void FindCreditCardsByUserLoginTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();
                User user = TestUtil.CreateExistentUser(language); CreditCard creditCard = TestUtil.CreateCreditCard(user);

                List<CreditCard> foundCreditCards = TestUtil.creditCardDao.FindCreditCardsByUserId(user.id);

                Assert.AreEqual(creditCard.id, foundCreditCards[0].id);
                Assert.AreEqual(creditCard.creditCardNumber, foundCreditCards[0].creditCardNumber);
                Assert.AreEqual(creditCard.creditType, foundCreditCards[0].creditType);
                Assert.AreEqual(creditCard.cvv, foundCreditCards[0].cvv);
                Assert.AreEqual(creditCard.expirationDate, foundCreditCards[0].expirationDate);
            }
        }
    }
}
