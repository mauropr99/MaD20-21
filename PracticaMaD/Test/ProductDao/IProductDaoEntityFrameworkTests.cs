using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao.Tests
{
    [TestClass()]
    public class IProductDaoEntityFrameworkTests
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
            TestUtil.productDao = kernel.Get<IProductDao>();
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
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
            Category category = TestUtil.CreateCategory("Ordenadores");

            Product product1 = TestUtil.CreateProduct(category, "Portatil Acer blanco", 3);
            Product product2 = TestUtil.CreateProduct(category, "Ordenador Acer sobremesa i7 RTX2600", 850);
            Product product3 = TestUtil.CreateProduct(category, "Portatil Toshiba reacondicionado", 3);

            List<Product> foundProducts = new List<Product>();
            foundProducts = TestUtil.productDao.FindByProductName("Acer", 0, 10);

            Assert.AreEqual(2, foundProducts.Count);
            //Están ordenados alfabéticamente por nombre, por eso que sea primero el [1]
            Assert.AreEqual(product1.product_name, foundProducts[1].product_name);
            Assert.AreEqual(product1.price, foundProducts[1].price);   
            Assert.AreEqual(product1.releaseDate, foundProducts[1].releaseDate);
            Assert.AreEqual(product1.stock, foundProducts[1].stock);
            Assert.AreEqual(product2.product_name, foundProducts[0].product_name);
            Assert.AreEqual(product2.price, foundProducts[0].price);
            Assert.AreEqual(product2.releaseDate, foundProducts[0].releaseDate);
            Assert.AreEqual(product2.stock, foundProducts[0].stock);
        }

        [TestMethod()]
        public void FindZeroByProductNameTest()
        {
            Category category = TestUtil.CreateCategory("Ordenadores");

            Product product1 = TestUtil.CreateProduct(category, "Portátil Acer blanco", 3);
            Product product2 = TestUtil.CreateProduct(category, "Ordenador Acer sobremesa i7 RTX2600", 850);
            Product product3 = TestUtil.CreateProduct(category, "Portatil Toshiba reacondicionado", 3);

            List<Product> foundProducts = new List<Product>();
            foundProducts = TestUtil.productDao.FindByProductName("Secador de pelo", 0, 10);

            Assert.AreEqual(0, foundProducts.Count);
        }

        [TestMethod()]
        public void FindByProductNameAndCategoryIdTest()
        {
            Category category = TestUtil.CreateCategory("Sobremesa");

            Category category2 = TestUtil.CreateCategory("Portátiles");

            Product product1 = TestUtil.CreateProduct(category2, "Portátil Acer blanco", 3);
            Product product2 = TestUtil.CreateProduct(category, "Ordenador Acer sobremesa i7 RTX2600", 850);
            Product product3 = TestUtil.CreateProduct(category2, "Portatil Toshiba reacondicionado", 3);

            List<Product> foundProducts = new List<Product>();
            foundProducts = TestUtil.productDao.FindByProductNameAndCategoryName("Acer", category.name, 0, 10);

            Assert.AreEqual(1, foundProducts.Count);
            Assert.AreEqual(product2.price, foundProducts[0].price);
            Assert.AreEqual(product2.product_name, foundProducts[0].product_name);
            Assert.AreEqual(product2.releaseDate, foundProducts[0].releaseDate);
            Assert.AreEqual(product2.stock, foundProducts[0].stock);

        }

        [TestMethod()]
        public void FindZeroByProductNameAndCategoryIdTest()
        {
            Category category = TestUtil.CreateCategory("Hogar");

            Category category2 = TestUtil.CreateCategory("Informática");

            Product product1 = TestUtil.CreateProduct(category2, "Portátil Acer blanco", 3);
            Product product2 = TestUtil.CreateProduct(category2, "Ordenador Acer sobremesa i7 RTX2600", 850);
            Product product3 = TestUtil.CreateProduct(category2, "Portatil Toshiba reacondicionado", 3);

            List<Product> foundProducts = new List<Product>();
            foundProducts = TestUtil.productDao.FindByProductNameAndCategoryName("Acer", category.name, 0, 10);

            Assert.AreEqual(0, foundProducts.Count);
        }

    }
}
