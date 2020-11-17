using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;


namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao.Tests
{
    [TestClass()]
    public class ICreditCardDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ICreditCardDao creditCardDao;

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
            creditCardDao = kernel.Get<ICreditCardDao>();
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


        //TODO hacer test de FindCreditCardsByUserLogin cuando se haga merge con develop.

    }
}
