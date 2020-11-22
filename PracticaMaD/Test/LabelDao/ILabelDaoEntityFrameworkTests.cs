using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao.Tests
{
    [TestClass()]
    public class ILabelDaoEntityFrameworkTests
    {
        private static IKernel kernel;

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
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();
            TestUtil.labelDao = kernel.Get<ILabelDao>();
            TestUtil.userDao = kernel.Get<IUserDao>();

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
        public void ExistsByLabelNameTest()
        {
            Category category1 = TestUtil.CreateCategory("Books");
            Book product1 = TestUtil.CreateBook(category1, "El Quijote Nueva edición", 3.5m, "El Quijote");
        }
    }
}

