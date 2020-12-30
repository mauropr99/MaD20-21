using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao.Tests
{
    [TestClass()]
    public class IBookDaoEntityFrameworkTests
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

            Category category1 = TestUtil.CreateCategory("Books");

            Book product1 = TestUtil.CreateBook(category1, "El Quijote", 3.5m, "Miguel de cervantes");
            Book product2 = TestUtil.CreateBook(category1, "Viaje al mundo de la fantasía", 3.5m, "Geronimo Stilton");
            Book product3 = TestUtil.CreateBook(category1, "Viaje al mundo de la fantasía 2", 3.5m, "Tea Stilton");

            List<Book>  foundBooks = TestUtil.bookDao.FindByProductName("mundo de la fantasía", 0, 10);

            Assert.AreEqual(2, foundBooks.Count);

            Assert.AreEqual(product2.product_name, foundBooks[0].product_name);
            Assert.AreEqual(product2.price, foundBooks[0].price);
            Assert.AreEqual(product2.releaseDate, foundBooks[0].releaseDate);
            Assert.AreEqual(product2.stock, foundBooks[0].stock);
            Assert.AreEqual(product2.genre, foundBooks[0].genre);
            Assert.AreEqual(product2.author, foundBooks[0].author);
            Assert.AreEqual(product2.isbnCode, foundBooks[0].isbnCode);
            Assert.AreEqual(product2.editorial, foundBooks[0].editorial);

            Assert.AreEqual(product3.product_name, foundBooks[1].product_name);
            Assert.AreEqual(product3.price, foundBooks[1].price);
            Assert.AreEqual(product3.releaseDate, foundBooks[1].releaseDate);
            Assert.AreEqual(product3.stock, foundBooks[1].stock);
            Assert.AreEqual(product3.genre, foundBooks[1].genre);
            Assert.AreEqual(product3.author, foundBooks[1].author);
            Assert.AreEqual(product3.isbnCode, foundBooks[1].isbnCode);
            Assert.AreEqual(product3.editorial, foundBooks[1].editorial);
        }

        [TestMethod()]
        public void FindZeroByProductNameTest()
        {
            Category category1 = TestUtil.CreateCategory("Books");

            Book product1 = TestUtil.CreateBook(category1, "El Quijote", 3.5m, "Miguel de cervantes");
            Book product2 = TestUtil.CreateBook(category1, "Viaje al mundo de la fantasía", 3.5m, "Geronimo Stilton");
            Book product3 = TestUtil.CreateBook(category1, "Viaje al mundo de la fantasía 2", 3.5m, "Tea Stilton");


            List<Book> foundBooks = TestUtil.bookDao.FindByProductName("Libro de cocina de Chicote", 0, 10);

            Assert.AreEqual(0, foundBooks.Count);
        }
    }
}