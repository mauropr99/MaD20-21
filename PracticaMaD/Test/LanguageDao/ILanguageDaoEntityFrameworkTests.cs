using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao.Tests
{
    [TestClass()]
    public class ILanguageDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ILanguageDao languageDao;

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_LANGUAGE_NAME = "non_existent_language";
        private const string NON_EXISTENT_LANGUAGE_COUNTRY = "non_existent_country";

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
        public void FindByNameAndCountryTest()
        {
            using (var scope = new TransactionScope())
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
                String expectedLanguageCountry = "ES";
                Language foundlanguage = languageDao.FindByNameAndCountry(expectedLanguageName, expectedLanguageCountry);

                Assert.AreEqual(foundlanguage.name, expectedLanguageName);
            }

        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLanguageNameTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = new Language
                {
                    name = "es",
                    country = "ES"
                };
                languageDao.Create(language);

                Language expectedLanguage = languageDao.FindByNameAndCountry(NON_EXISTENT_LANGUAGE_NAME, "ES");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLanguageCountryTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = new Language
                {
                    name = "es",
                    country = "ES"
                };
                languageDao.Create(language);

                Language expectedLanguage = languageDao.FindByNameAndCountry("es", NON_EXISTENT_LANGUAGE_COUNTRY);
            }
        }
    }
}
