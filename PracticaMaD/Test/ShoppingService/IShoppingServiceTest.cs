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
        private const string languageName = "es";
        private const string languageCountry = "ES";
        private const string role = "user";
        private static Language language = new Language();

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

        [TestMethod()]
        public void BuyProductTest()
        {
            using (var scope = new TransactionScope())
            {
                CreditCard creditCard = new CreditCard();
                creditCard = TestUtil.CreateCreditCard(creditCard);
                userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name, address));
                Category category = new Category();
                TestUtil.CreateCategory(category, "Balones");
                Product product1 = new Product();
                TestUtil.CreateProduct(category, "Balón negro", 3, product1);
                Product product2 = new Product();
                TestUtil.CreateProduct(category, "Balón blanco", 2.5m, product2);

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.product_name,
                    15,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.product_name,
                   100,
                   product2.price
               );
                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>();
                orderLineDetails.Add(orderLineDetail1);
                orderLineDetails.Add(orderLineDetail2);

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, address), orderLineDetails,
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
                userService.SingUpUser(login, password,
                new UserDetails(name, lastName, email, language.name, address));
                CreditCard creditCard = new CreditCard();
                creditCard.creditType = "debit";
                creditCard.creditCardNumber = "1234567891234567";
                creditCard.cvv = 123;
                creditCard.expirationDate = DateTime.Now.AddYears(-1);

                TestUtil.creditCardDao.Create(creditCard);
                Category category = new Category();
                TestUtil.CreateCategory(category, "Balones");
                Product product1 = new Product();
                TestUtil.CreateProduct(category, "Balón negro", 3, product1);
                Product product2 = new Product();
                TestUtil.CreateProduct(category, "Balón blanco", 2.5m, product2);

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.product_name,
                    15,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.product_name,
                   100,
                   product2.price
               );
                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>();
                orderLineDetails.Add(orderLineDetail1);
                orderLineDetails.Add(orderLineDetail2);

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindByNonExistentLoginTest()
        {
            using (var scope = new TransactionScope())
            {

                CreditCard creditCard = new CreditCard();
                creditCard = TestUtil.CreateCreditCard(creditCard);
                Category category = new Category();
                TestUtil.CreateCategory(category, "Balones");
                Product product1 = new Product();
                TestUtil.CreateProduct(category, "Balón negro", 3, product1);
                Product product2 = new Product();
                TestUtil.CreateProduct(category, "Balón blanco", 2.5m, product2);
              
                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.product_name,
                    15,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.product_name,
                   100,
                   product2.price
               );
                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>();
                orderLineDetails.Add(orderLineDetail1);
                orderLineDetails.Add(orderLineDetail2);

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NotEnoughStock))]
        public void NotEnoughStockTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.SingUpUser(login, password,
                      new UserDetails(name, lastName, email, language.name, address));
                CreditCard creditCard = new CreditCard();
                creditCard = TestUtil.CreateCreditCard(creditCard);
                Category category = new Category();
                TestUtil.CreateCategory(category, "Mascarillas");
                Product product1 = new Product();
                TestUtil.CreateProduct(category, "FFP2", 3, product1);
                Product product2 = new Product();
                TestUtil.CreateProduct(category, "Quirurjica", 2.5m, product2);

                OrderLineDetails orderLineDetail1 = new OrderLineDetails(
                    product1.product_name,
                    1000,
                    product1.price
                );
                OrderLineDetails orderLineDetail2 = new OrderLineDetails(
                   product2.product_name,
                   100,
                   product2.price
               );
                List<OrderLineDetails> orderLineDetails = new List<OrderLineDetails>();
                orderLineDetails.Add(orderLineDetail1);
                orderLineDetails.Add(orderLineDetail2);

                var order =
                    shoppingService.BuyProducts(new UserDetails(name, lastName, email, language.name, address), orderLineDetails,
                        address, creditCard, "Patatas asadas");
            }
        }

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            shoppingService = kernel.Get<IShoppingService>();
            userService = kernel.Get<IUserService>();

            TestUtil.userDao = kernel.Get<IUserDao>();
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
            TestUtil.orderDao = kernel.Get<IOrderDao>();
            TestUtil.orderLineDao = kernel.Get<IOrderLineDao>();
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();
            TestUtil.productDao = kernel.Get<IProductDao>();

            language.name = languageName;
            language.country = languageCountry;
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

        #endregion Additional test 
        
    }
}