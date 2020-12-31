using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ComputerDao.Tests
{
    [TestClass()]
    public class IComputerDaoEntityFrameworkTests
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
            TestUtil.computerDao = kernel.Get<IComputerDao>();

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
        public void FindByProductNameTest()
        {
            using (var scope = new TransactionScope())
            {

                Category category1 = TestUtil.CreateCategory("Computers");

                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Computer product3 = TestUtil.CreateComputer(category1, "ACER 4x2600", 2.5m, "Acer");


                List<Computer> foundComputers = TestUtil.computerDao.FindByProductName("Acer", 0, 10);

                Assert.AreEqual(2, foundComputers.Count);

                Assert.AreEqual(product2.product_name, foundComputers[0].product_name);
                Assert.AreEqual(product2.price, foundComputers[0].price);
                Assert.AreEqual(product2.releaseDate, foundComputers[0].releaseDate);
                Assert.AreEqual(product2.stock, foundComputers[0].stock);
                Assert.AreEqual(product2.stock, foundComputers[0].stock);
                Assert.AreEqual(product2.brand, foundComputers[0].brand);
                Assert.AreEqual(product2.processor, foundComputers[0].processor);
                Assert.AreEqual(product2.os, foundComputers[0].os);

                Assert.AreEqual(product3.product_name, foundComputers[1].product_name);
                Assert.AreEqual(product3.price, foundComputers[1].price);
                Assert.AreEqual(product3.releaseDate, foundComputers[1].releaseDate);
                Assert.AreEqual(product3.stock, foundComputers[1].stock);
                Assert.AreEqual(product3.brand, foundComputers[1].brand);
                Assert.AreEqual(product3.processor, foundComputers[1].processor);
                Assert.AreEqual(product3.os, foundComputers[1].os);
            }
        }

        [TestMethod()]
        public void FindZeroByProductNameTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Computers");

                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Computer product3 = TestUtil.CreateComputer(category1, "ACER 4x2600", 2.5m, "Acer");


                List<Computer> foundComputers = TestUtil.computerDao.FindByProductName("MacBook Pro 2020", 0, 10);

                Assert.AreEqual(0, foundComputers.Count);
            }
        }
    }
}