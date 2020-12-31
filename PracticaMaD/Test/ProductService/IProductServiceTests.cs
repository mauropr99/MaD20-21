using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService.Tests
{
    /// <summary>
    ///This is a test class for IAccountServiceTest and is intended
    ///to contain all IAccountServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IProductServiceTest
    {
        private static IKernel kernel;
        private static IProductService productService;

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
            TestUtil.productDao = kernel.Get<IProductDao>();
            TestUtil.computerDao = kernel.Get<IComputerDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();

            productService = kernel.Get<IProductService>();

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
        public void ViewCatalogTest()
        {
            using (var scope = new TransactionScope())
            {

                Category category1 = TestUtil.CreateCategory("Computers");
                Category category2 = TestUtil.CreateCategory("Books");

                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Book product2 = TestUtil.CreateBook(category2, "El Quijote Nueva edición", 3.5m, "El Quijote");

                ProductBlock catalog = productService.ViewCatalog("Msi", category1.name, 0, 10);

                Assert.AreEqual(1, catalog.Products.Count);
                Assert.AreEqual(catalog.Products[0].Id, product1.id);
                Assert.AreEqual(catalog.Products[0].Price, product1.price);
                Assert.AreEqual(catalog.Products[0].Stock, product1.stock);
            }
        }

        [TestMethod()]
        public void UpdateProductTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Computers");

                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                product1.stock = 20;
                product1.price = 15m;

                productService.UpdateComputer(product1);
                Product updatedProduct = TestUtil.productDao.Find(product1.id);

                Assert.AreEqual(product1.stock, updatedProduct.stock);
                Assert.AreEqual(product1.price, updatedProduct.price);
            }
        }

        [TestMethod()]
        public void FindAllCategtoriesTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Computers");
                Category category2 = TestUtil.CreateCategory("Books");

                List<Category> foundedCategories = productService.ViewAllCategories();

                Assert.AreEqual(2, foundedCategories.Count);
                Assert.AreEqual(category1.name, foundedCategories[0].name);
                Assert.AreEqual(category2.name, foundedCategories[1].name);
            }
        }

        [TestMethod()]
        public void FindBookTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Books");

                Book book = TestUtil.CreateBook(category1, "Libro_de_bolsillo", 10, "Titulo_del_libro");

                ProductBlock productBlock = productService.ViewCatalog("Libro_de_bolsillo", 0, 1);

                long productId = productBlock.Products[0].Id;

                Book foundBook = productService.FindBook(productId);

                Assert.IsTrue(book.Equals(foundBook));
            }

        }

        [TestMethod()]
        public void FindComputerTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Books");

                Computer computer = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");

                ProductBlock productBlock = productService.ViewCatalog("Msi GL 62 6QD", 0, 1);

                long productId = productBlock.Products[0].Id;

                Computer foundComputer = productService.FindComputer(productId);

                Assert.IsTrue(computer.Equals(foundComputer));

            }
        }
    }
}
