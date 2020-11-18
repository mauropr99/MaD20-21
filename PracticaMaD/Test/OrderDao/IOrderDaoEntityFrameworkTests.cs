using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Ninject;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao.Tests
{
    [TestClass()]
    public class IOrderDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static IOrderDao orderDao;
        private static IOrderLineDao orderLineDao;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static IUserDao userDao;
        private static ILanguageDao languageDao;
        private static ICreditCardDao creditCardDao;

        private const long NON_EXISTENT_ID_USER = -1;

        private static Language createExistentLanguage(Language language)
        {
            language.name = "español";
            language.country = "España";
            languageDao.Create(language);

            return language;
        }

        private static User createExistentUser(User user, Language language)
        {

            user.login = "user";
            user.name = "usuario";
            user.lastName = "dePrueba";
            user.password = "passwd";
            user.address = "A Coruña";
            user.email = "user@user";
            user.role = "user";
            user.languageId = language.id;
            userDao.Create(user);

            return user;
        }

        private static Order createOrder(User user,CreditCard credirCard, List<OrderLine> orderLines ,Order order)
        {
            order.postalAddress = "A Coruña";
            foreach(OrderLine orderLine in orderLines)
            {
                order.totalPrice = orderLine.price + order.totalPrice;
            }
            order.description = "Regalo para mauro";
            order.creditCardId = credirCard.id;
            order.userId = user.id;
            orderDao.Create(order);
            foreach (OrderLine orderLine in orderLines)
            {
                orderLine.orderId = order.id;
                orderLineDao.Create(orderLine);
            }
            return order;
        }

        private static OrderLine createOrderLine(Product product,OrderLine orderLine)
        {
            orderLine.quantity = 2;
            orderLine.price = product.price;
            orderLine.productId = product.id;

            orderLineDao.Create(orderLine);
            return orderLine;
        }

        private static Product createProduct(Category category,string productName, decimal price, Product product)
        {
            product.product_name = productName;
            product.price = price;
            product.releaseDate = DateTime.Now;
            product.stock = 10000;
            product.categoryId = category.id;

            productDao.Create(product);
            return product;
        }

        private static Category createCategory(Category category)
        {
            category.name = "Balones";

            categoryDao.Create(category);
            return category;
        }

        private static CreditCard createCreditCard(CreditCard creditCard)
        {

            creditCard.creditType = "debit";
            creditCard.creditCardNumber = "1234567891234567";
            creditCard.cvv = 123;
            creditCard.expirationDate = DateTime.Now.AddYears(1);

            creditCardDao.Create(creditCard);
            return creditCard;
        }
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
            userDao = kernel.Get<IUserDao>();
            languageDao = kernel.Get<ILanguageDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            orderDao = kernel.Get<IOrderDao>();
            orderLineDao = kernel.Get<IOrderLineDao>();
            creditCardDao = kernel.Get<ICreditCardDao>();
            productDao = kernel.Get<IProductDao>();
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
        public void FindByUserIdTest()
        {
            Category category = new Category();
            createCategory(category);
            Product product1 = new Product();
            createProduct(category,"Balón negro", 3, product1);
            Product product2 = new Product();
            createProduct(category, "Balón blanco", 2.5m, product2);
            Product product3 = new Product();
            createProduct(category, "Balón negro y blanco", 3.5m, product3);
            OrderLine orderline1 = new OrderLine();
            createOrderLine(product1, orderline1);
            OrderLine orderline2 = new OrderLine();
            createOrderLine(product2, orderline2);
            OrderLine orderline3 = new OrderLine();
            createOrderLine(product3, orderline3);
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(orderline1);
            orderLines.Add(orderline2);
            Language language = new Language();
            createExistentLanguage(language);
            User user = new User();
            createExistentUser(user, language);
            CreditCard creditCard = new CreditCard();
            createCreditCard(creditCard);
            Order order1 = new Order();
            createOrder(user,creditCard,orderLines,order1);
            List<OrderLine> orderLines2 = new List<OrderLine>();
            orderLines.Add(orderline3);
            Order order2 = new Order();
            createOrder(user, creditCard, orderLines2, order2);

            List<Order> foundOrders = new List<Order>();
            foundOrders = orderDao.FindByUserId(user.id,0,10);

            Assert.AreEqual(2,foundOrders.Count);
            Assert.AreEqual(order1.orderDate, foundOrders[0].orderDate);
            Assert.AreEqual(order1.totalPrice, foundOrders[0].totalPrice);
            Assert.AreEqual(order1.userId, foundOrders[0].userId);
            Assert.AreEqual(order1.totalPrice, foundOrders[0].totalPrice);
            Assert.AreEqual(order1.description, foundOrders[0].description);Assert.AreEqual(2,foundOrders.Count);
            Assert.AreEqual(order2.orderDate, foundOrders[1].orderDate);
            Assert.AreEqual(order2.totalPrice, foundOrders[1].totalPrice);
            Assert.AreEqual(order2.userId, foundOrders[1].userId);
            Assert.AreEqual(order2.totalPrice, foundOrders[1].totalPrice);
            Assert.AreEqual(order2.description, foundOrders[1].description);

            foundOrders = orderDao.FindByUserId(NON_EXISTENT_ID_USER, 0, 10);

            Assert.AreEqual(0, foundOrders.Count);

            Order foundOrder = new Order();
            foundOrder = orderDao.Find(order1.id);

            Assert.AreEqual(order1.orderDate, foundOrder.orderDate);
            Assert.AreEqual(order1.totalPrice, foundOrder.totalPrice);
            Assert.AreEqual(order1.userId, foundOrder.userId);
            Assert.AreEqual(order1.totalPrice, foundOrder.totalPrice);
            Assert.AreEqual(order1.description, foundOrder.description);
        }
    }
}
