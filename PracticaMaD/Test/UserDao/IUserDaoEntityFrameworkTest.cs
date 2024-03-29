﻿using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao.Test
{
    [TestClass()]
    public class IUserDaoEntityFrameworkTest
    {

        private static IKernel kernel;

        private const string NON_EXISTENT_USER = "no_user";
        private static Language language;
        private static User user;

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


            language = TestUtil.CreateExistentLanguage();
            user = TestUtil.CreateExistentUser(language);
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
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();
                User user = TestUtil.CreateExistentUser(language);

                User foundUser = foundUser = TestUtil.userDao.FindByLogin("user");

                Assert.AreEqual(user.id, foundUser.id);
                Assert.AreEqual(user.login, foundUser.login);
                Assert.AreEqual(user.name, foundUser.name);
                Assert.AreEqual(user.lastName, foundUser.lastName);
                Assert.AreEqual(user.password, foundUser.password);
                Assert.AreEqual(user.email, foundUser.email);
                Assert.AreEqual(user.languageId, foundUser.languageId);
                Assert.AreEqual(user.role, foundUser.role);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLoginTest()
        {
            using (var scope = new TransactionScope())
            {
                User foundUser = TestUtil.userDao.FindByLogin(NON_EXISTENT_USER);
            }
        }
    }
}


