using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao.Tests
{
    [TestClass()]
    public class IProductDaoEntityFrameworkTests
    {
        private static IKernel kernel;

        private TransactionScope transactionScope;
        private static ICommentService commentService;
        private static IUserService userService;
        private static Language language;
        private static User user;

        private const string login = "user";
        private const string login2 = "user2";
        private const string name = "name";
        private const string lastName = "lastName";
        private const string password = "passwd";
        private const string email = "user@udc.es";
        private const string email2 = "user2@udc.es";
        private const string address = "A Coruña";
        private const string role = "user";

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
            TestUtil.computerDao = kernel.Get<IComputerDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();
            TestUtil.languageDao = kernel.Get<ILanguageDao>();
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();
            TestUtil.userDao = kernel.Get<IUserDao>();


            commentService = kernel.Get<ICommentService>();
            userService = kernel.Get<IUserService>();

            language = TestUtil.CreateExistentLanguage();
            user = TestUtil.CreateExistentUser(language);

        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestUtil.userDao.Remove(user.id);
            TestUtil.languageDao.Remove(language.id);
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
                Category category1 = TestUtil.CreateCategory("Ordenadores");

                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Computer product3 = TestUtil.CreateComputer(category1, "ACER 4x2600", 2.5m, "Acer");


                List<Product> foundProducts = new List<Product>();
                foundProducts = TestUtil.productDao.FindByProductName("Acer", 0, 10);

                Assert.AreEqual(2, foundProducts.Count);
                //Están ordenados alfabéticamente por nombre, por eso que sea primero el [1]
                Assert.AreEqual(product3.product_name, foundProducts[1].product_name);
                Assert.AreEqual(product3.price, foundProducts[1].price);
                Assert.AreEqual(product3.releaseDate, foundProducts[1].releaseDate);
                Assert.AreEqual(product3.stock, foundProducts[1].stock);
                Assert.AreEqual(product2.product_name, foundProducts[0].product_name);
                Assert.AreEqual(product2.price, foundProducts[0].price);
                Assert.AreEqual(product2.releaseDate, foundProducts[0].releaseDate);
                Assert.AreEqual(product2.stock, foundProducts[0].stock);
            }
        }

        [TestMethod()]
        public void FindZeroByProductNameTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Category category2 = TestUtil.CreateCategory("Libros");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Book product3 = TestUtil.CreateBook(category2, "El Quijote Nueva edición", 3.5m, "El Quijote");

                List<Product> foundProducts = new List<Product>();
                foundProducts = TestUtil.productDao.FindByProductName("Secador de pelo", 0, 10);

                Assert.AreEqual(0, foundProducts.Count);
            }
        }

        [TestMethod()]
        public void FindByProductNameAndCategoryIdTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Category category2 = TestUtil.CreateCategory("Libros");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Book product3 = TestUtil.CreateBook(category2, "El Quijote Nueva edición", 3.5m, "El Quijote");

                List<Product> foundProducts = new List<Product>();
                foundProducts = TestUtil.productDao.FindByProductNameAndCategoryName("Acer", category1.name, 0, 10);

                Assert.AreEqual(1, foundProducts.Count);
                Assert.AreEqual(product2.price, foundProducts[0].price);
                Assert.AreEqual(product2.product_name, foundProducts[0].product_name);
                Assert.AreEqual(product2.releaseDate, foundProducts[0].releaseDate);
                Assert.AreEqual(product2.stock, foundProducts[0].stock);
            }

        }

        [TestMethod()]
        public void FindZeroByProductNameAndCategoryIdTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Category category2 = TestUtil.CreateCategory("Libros");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Book product3 = TestUtil.CreateBook(category2, "El Quijote Nueva edición", 3.5m, "El Quijote");

                List<Product> foundProducts = new List<Product>();
                foundProducts = TestUtil.productDao.FindByProductNameAndCategoryName("Acer", category2.name, 0, 10);

                Assert.AreEqual(1, foundProducts.Count);
            }
        }

        [TestMethod()]
        public void FindByLabelTest()
        {
            using (var scope = new TransactionScope())
            {
                CreditCard creditCard = TestUtil.CreateCreditCard();

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER GL 62 6QD", 3, "ACER");
                List<string> labels = new List<string>
                {
                    "Ganga",
                    "Oferta",
                    "Chollazo"
                };

                string text = "Muy buen ordenador y a buen precio. Funcionan todos los juegos a calidad máxima, muy fluidos y sin apenas calentarse el aparato.";
                var comment1 = commentService.NewComment(user.id, product1.id, text, labels);

                text = "Es una bestia de portatil gaming , los juegos se ven genial y se inician en un momento , no se calienta y encima no es tan pesado .";

                List<string> labels2 = new List<string>
                {
                    "Irresistible",
                    "Chollazo",
                };
                var comment2 = commentService.NewComment(user.id, product2.id, text, labels2);
                List<Product> products = new List<Product>();
                products.Add(product1);
                products.Add(product2);
                List<Product> productsFound = new List<Product>();
                productsFound = TestUtil.productDao.FindByLabel("Chollazo", 0, 5);
                Assert.AreEqual(products.Count, productsFound.Count);
                products.Clear();
                products.Add(product2);
                productsFound = TestUtil.productDao.FindByLabel("Irresistible", 0, 5);
                Assert.AreEqual(products[0].id, productsFound[0].id);
            }
        }
    }
}
