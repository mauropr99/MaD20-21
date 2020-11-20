using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao.Tests
{
    [TestClass()]
    public class ILanguageDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ILanguageDao languageDao;

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_LANGUAGE_NAME = "non_existent_language";

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
        public void FindByNameTest()
        {
            int numberOfLanguages = 11;

            List<Language> createdLanguage = new List<Language>(numberOfLanguages);

            /* Create 11 numberOfLanguages */
            for (int i = 0; i < numberOfLanguages; i++)
            {
                Language language = new Language
                {
                    name = "language " + i.ToString(),
                    country = "ES"
                };
                languageDao.Create(language);
                createdLanguage.Add(language);
            }

            String expectedLanguageName = "language 8";
            Language foundlanguage = languageDao.FindByName(expectedLanguageName);

            Assert.AreEqual(foundlanguage.name, expectedLanguageName);

        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLanguageNameTest()
        {
            Language expectedLanguage = languageDao.FindByName(NON_EXISTENT_LANGUAGE_NAME);
        }
    }
}
