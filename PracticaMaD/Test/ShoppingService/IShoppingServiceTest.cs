using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Tests
{
    /// <summary>
    ///This is a test class for IAccountServiceTest and is intended
    ///to contain all IAccountServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IShoppingServiceTest
    {
        private static IKernel kernel;
        private static IShoppingService shoppingService;
        private static IUserService userService;

        private const string login = "user";
        private const string name = "name";
        private const string lastName = "lastName";
        private const string password = "passwd";
        private const string email = "user@udc.es";
        private const string address = "A Coruña";
        private const string role = "user";

        // Variables used in several tests are initialized here
        private const long userId = 123456;

        private const long NON_EXISTENT_ACCOUNT_ID = -1;
        private const long NON_EXISTENT_USER_ID = -1;

        //Due to the limited precision of floating point numbers, the equality
        //operator may provide unexpected results if two numbers are close to
        //each other (e.g. 25 and 25.00000000001). In order to solve this
        //issue, a small margin of error (delta) can be allowed.
        private const double delta = 0.00001;

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
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
            TestUtil.productDao = kernel.Get<IProductDao>();
            TestUtil.orderDao = kernel.Get<IOrderDao>();
            TestUtil.computerDao = kernel.Get<IComputerDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();


            shoppingService = kernel.Get<IShoppingService>();
            userService = kernel.Get<IUserService>();

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
        public void BuyProductTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

                long userId = userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name, language.country, address));
                User user = TestUtil.userDao.Find(userId);
                CreditCard creditCard = TestUtil.CreateCreditCard(user);

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
 

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.id,
                    product1.product_name,
                    15,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.id,
                   product2.product_name,
                   100,
                   product2.price
               );

                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2
                };

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, language.country, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");

                var foundOrder = TestUtil.orderDao.Find(order.id);

                Assert.AreEqual(order.id, foundOrder.id);
                Assert.AreEqual(address, foundOrder.postalAddress);
                Assert.AreEqual(order.orderDate, foundOrder.orderDate);
                Assert.AreEqual(order.totalPrice, foundOrder.totalPrice);
                Assert.AreEqual(order.userId, foundOrder.userId);
                Assert.AreEqual(order.creditCardId, foundOrder.creditCardId);
                Assert.AreEqual(order.description, foundOrder.description);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(CreditCardAlreadyExpired))]
        public void CreditCardAlreadyExpiredTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

                userService.SingUpUser(login, password,
                new UserDetails(name, lastName, email, language.name, language.country, address));
                CreditCard creditCard = new CreditCard
                {
                    ownerName = "Name Surname",
                    creditType = "debit",
                    creditCardNumber = "1234567891234567",
                    cvv = 123,
                    expirationDate = DateTime.Now.AddYears(-1)
                };

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.id,
                    product1.product_name,
                    15,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.id,
                   product2.product_name,
                   100,
                   product2.price
               );
                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2
                };

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, language.country, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
            }
        }


        [TestMethod()]
        [ExpectedException(typeof(NotEnoughStock))]
        public void NotEnoughStockTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

                long userId = userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name, language.country, address));
                User user = TestUtil.userDao.Find(userId);
                CreditCard creditCard = TestUtil.CreateCreditCard(user);
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                   product2.id,
                   product2.product_name,
                   90,
                   product2.price
               );

                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>
                {
                    orderLineDetail1
                };

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, language.country, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
                order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, language.country, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
            }
        }

        
        
    }
}