using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

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
        private static Language language;
        private static User user;

        private const string login = "user";
        private const string name = "name";
        private const string lastName = "lastName";
        private const string password = "passwd";
        private const string email = "user@udc.es";
        private const string address = "A Coruña";
        private const string role = "user";

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
            TestUtil.orderLineDao = kernel.Get<IOrderLineDao>();
            TestUtil.computerDao = kernel.Get<IComputerDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();


            shoppingService = kernel.Get<IShoppingService>();
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
        public void BuyProductTest()
        {
            using (var scope = new TransactionScope())
            {
                CreditCard creditCard = TestUtil.CreateCreditCard();

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");


                ShoppingCartDetails orderLineDetail1 = new ShoppingCartDetails(
                    product1.id,
                    product1.product_name,
                    category1.name,
                    15,
                    product1.price,
                    true
                );
                ShoppingCartDetails orderLineDetail2 = new ShoppingCartDetails(
                   product2.id,
                   product2.product_name,
                   category1.name,
                   100,
                   product2.price,
                   false
               );

                List<ShoppingCartDetails> orderLineDetails = new List<ShoppingCartDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2
                };

                var order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, "Patatas asadas");

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
                CreditCard creditCard = new CreditCard
                {
                    ownerName = "Name Surname",
                    creditType = "debit",
                    creditCardNumber = "1234567891234567",
                    cvv = 123,
                    expirationDate = DateTime.Now.AddYears(-1)
                };
                TestUtil.creditCardDao.Create(creditCard);

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");

                ShoppingCartDetails orderLineDetail1 = new ShoppingCartDetails(
                    product1.id,
                    product1.product_name,
                    category1.name,
                    15,
                    product1.price,
                    true
                );
                ShoppingCartDetails orderLineDetail2 = new ShoppingCartDetails(
                   product2.id,
                   product2.product_name,
                   category1.name,
                   100,
                   product2.price,
                   false
               );

                List<ShoppingCartDetails> orderLineDetails = new List<ShoppingCartDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2
                };

                var order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, "Patatas asadas");
            }
        }


        [TestMethod()]
        [ExpectedException(typeof(NotEnoughStock))]
        public void NotEnoughStockTest()
        {
            using (var scope = new TransactionScope())
            {
                CreditCard creditCard = TestUtil.CreateCreditCard();
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");

                ShoppingCartDetails orderLineDetail1 = new ShoppingCartDetails(
                   product2.id,
                   product2.product_name,
                   category1.name,
                   90,
                   product2.price,
                   false
               );

                List<ShoppingCartDetails> orderLineDetails = new List<ShoppingCartDetails>
                {
                    orderLineDetail1
                };

                var order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, "Patatas asadas");
                order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, "Patatas asadas");
            }
        }

        [TestMethod()]
        public void FindOrdersByUserIdTest()
        {
            using (var scope = new TransactionScope())
            {
                //Creating CreditCard...
                CreditCard creditCard = TestUtil.CreateCreditCard();

                //Creating Category...
                Category category1 = TestUtil.CreateCategory("Ordenadores");

                //Products
                Computer product1 = TestUtil.CreateComputer(category1, "Computer 1", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "Computer 2", 3, "Acer");
                Computer product3 = TestUtil.CreateComputer(category1, "Computer 3", 3, "Msi");
                Computer product4 = TestUtil.CreateComputer(category1, "Computer 4", 3, "Acer");

                //Creating OrderLineDetails
                ShoppingCartDetails orderLineDetail1 = new ShoppingCartDetails(
                    product1.id,
                    product1.product_name,
                    category1.name,
                    10,
                    product1.price,
                    false
                );

                ShoppingCartDetails orderLineDetail2 = new ShoppingCartDetails(
                   product2.id,
                   product2.product_name,
                   category1.name,
                   11,
                   product2.price,
                   false
                );
                ShoppingCartDetails orderLineDetail3 = new ShoppingCartDetails(
                   product3.id,
                   product3.product_name,
                   category1.name,
                   12,
                   product3.price,
                   false
               );
                ShoppingCartDetails orderLineDetail4 = new ShoppingCartDetails(
                   product4.id,
                   product4.product_name,
                   category1.name, 
                   13,
                   product4.price,
                   false
               );

                List<ShoppingCartDetails> orderLineDetails = new List<ShoppingCartDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2
                };

                List<ShoppingCartDetails> orderLineDetails2 = new List<ShoppingCartDetails>
                {
                    orderLineDetail3,
                    orderLineDetail4
                };


                //Creating Orders...
                string firstDescription = "First order";
                string secondDescription = "Second order";
                var order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, firstDescription);

                var order2 =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, secondDescription);


                List<OrderDetails> foundOrders = shoppingService.FindOrdersByUserId(user.id, 0, 10).Orders;

                Order firstOrder = TestUtil.orderDao.Find(foundOrders[0].Id);
                Order secondOrder = TestUtil.orderDao.Find(foundOrders[1].Id);

                //Checking total number of orders
                Assert.AreEqual(foundOrders.Count, 2);

                Assert.AreEqual(foundOrders[0].Id, firstOrder.id);
                Assert.AreEqual(foundOrders[1].Id, secondOrder.id);
            }

        }

        [TestMethod()]
        public void ViewOrderLineDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                //Creating CreditCard...
                CreditCard creditCard = TestUtil.CreateCreditCard();

                //Creating Category...
                Category category1 = TestUtil.CreateCategory("Ordenadores");

                //Products
                Computer product1 = TestUtil.CreateComputer(category1, "Computer 1", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "Computer 2", 3, "Acer");
                Computer product3 = TestUtil.CreateComputer(category1, "Computer 3", 3, "Msi");
                Computer product4 = TestUtil.CreateComputer(category1, "Computer 4", 3, "Acer");

                ShoppingCartDetails orderLineDetail1 = new ShoppingCartDetails(
                    product1.id,
                    product1.product_name,
                    category1.name,
                    10,
                    product1.price,
                    false
                );

                ShoppingCartDetails orderLineDetail2 = new ShoppingCartDetails(
                   product2.id,
                   product2.product_name,
                   category1.name,
                   11,
                   product2.price,
                   false
                );
                ShoppingCartDetails orderLineDetail3 = new ShoppingCartDetails(
                   product3.id,
                   product3.product_name,
                   category1.name,
                   12,
                   product3.price,
                   false
               );
                ShoppingCartDetails orderLineDetail4 = new ShoppingCartDetails(
                   product4.id,
                   product4.product_name,
                   category1.name,
                   13,
                   product4.price,
                   false
               );

                List<ShoppingCartDetails> orderLineDetails = new List<ShoppingCartDetails>
                {
                    orderLineDetail1,
                    orderLineDetail2,
                    orderLineDetail3,
                    orderLineDetail4
                };
                

                //Creating Orders...
                string firstDescription = "First order";
                var order =
                    shoppingService.BuyProducts(user.id, orderLineDetails,
                        address, creditCard.id, firstDescription);

                var orderLines = TestUtil.orderLineDao.FindByOrderId(order.id);


                List<OrderLineDetails> foundOrderLineDetails = shoppingService.ViewOrderLineDetails(order.id);


                //Checking total number of orders
                Assert.AreEqual(orderLineDetails.Count, foundOrderLineDetails.Count);
                Assert.AreEqual(orderLineDetail1.Product_Id, foundOrderLineDetails[0].Product_Id);
                Assert.AreEqual(orderLineDetail2.Product_Id, foundOrderLineDetails[1].Product_Id);
                Assert.AreEqual(orderLineDetail3.Product_Id, foundOrderLineDetails[2].Product_Id);
                Assert.AreEqual(orderLineDetail4.Product_Id, foundOrderLineDetails[3].Product_Id);
                Assert.AreEqual(orderLineDetail1.Product_Name, foundOrderLineDetails[0].Product_Name);
                Assert.AreEqual(orderLineDetail2.Product_Name, foundOrderLineDetails[1].Product_Name);
                Assert.AreEqual(orderLineDetail3.Product_Name, foundOrderLineDetails[2].Product_Name);
                Assert.AreEqual(orderLineDetail4.Product_Name, foundOrderLineDetails[3].Product_Name);
                Assert.AreEqual(orderLineDetail1.Quantity, foundOrderLineDetails[0].Quantity);
                Assert.AreEqual(orderLineDetail2.Quantity, foundOrderLineDetails[1].Quantity);
                Assert.AreEqual(orderLineDetail3.Quantity, foundOrderLineDetails[2].Quantity);
                Assert.AreEqual(orderLineDetail4.Quantity, foundOrderLineDetails[3].Quantity);
                Assert.AreEqual(orderLineDetail1.Price, foundOrderLineDetails[0].Price);
                Assert.AreEqual(orderLineDetail2.Price, foundOrderLineDetails[1].Price);
                Assert.AreEqual(orderLineDetail3.Price, foundOrderLineDetails[2].Price);
                Assert.AreEqual(orderLineDetail4.Price, foundOrderLineDetails[3].Price);


            }
        }

    }
}