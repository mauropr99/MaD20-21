using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao.Tests
{
    [TestClass()]
    public class ICategoryDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_CATEGORY_NAME = "non_existent_cat";

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
            categoryDao = kernel.Get<ICategoryDao>();

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
        public void FindByCategoryNameTest()
        {
            using (var scope = new TransactionScope())
            {
                int numberOfCategories = 11;

                List<Category> createdCategories = new List<Category>(numberOfCategories);

                /* Create 11 categories */
                for (int i = 0; i < numberOfCategories; i++)
                {
                    Category category = new Category
                    {
                        name = "category " + i.ToString()
                    };
                    categoryDao.Create(category);
                    createdCategories.Add(category);
                }

                string expectedCategoryName = "category 7";
                Category foundCategory = categoryDao.FindByName(expectedCategoryName);

                Assert.AreEqual(foundCategory.name, expectedCategoryName);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentNameTest()
        {
            using (var scope = new TransactionScope())
            {
                Category foundCategory = categoryDao.FindByName(NON_EXISTENT_CATEGORY_NAME);
            }
        }
    }
}
