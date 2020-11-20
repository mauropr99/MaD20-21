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
using Es.Udc.DotNet.PracticaMaD.Test.Util;
namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao.Tests
{
    [TestClass()]
    public class IOrderDaoEntityFrameworkTests
    {
        private static IKernel kernel;

        private const long NON_EXISTENT_ID_USER = -1;

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
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
            TestUtil.orderDao = kernel.Get<IOrderDao>();
            TestUtil.orderLineDao = kernel.Get<IOrderLineDao>();
            TestUtil.creditCardDao = kernel.Get<ICreditCardDao>();
            TestUtil.productDao = kernel.Get<IProductDao>();
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
            Category category = TestUtil.CreateCategory("Balones");
            Product product1 = TestUtil.CreateProduct(category,"Balón negro", 3);
            Product product2 = TestUtil.CreateProduct(category, "Balón blanco", 2.5m);
            Product product3 = TestUtil.CreateProduct(category, "Balón negro y blanco", 3.5m);
            OrderLine orderline1 = TestUtil.CreateOrderLine(product1);
            OrderLine orderline2 = TestUtil.CreateOrderLine(product2);
            OrderLine orderline3 = TestUtil.CreateOrderLine(product3);
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(orderline1);
            orderLines.Add(orderline2);
            Language language = TestUtil.CreateExistentLanguage();
            User user = TestUtil.CreateExistentUser(language);
            CreditCard creditCard = TestUtil.CreateCreditCard();
            Order order1 = TestUtil.CreateOrder(user,creditCard,orderLines);
            List<OrderLine> orderLines2 = new List<OrderLine>();
            orderLines.Add(orderline3);
            Order order2 = TestUtil.CreateOrder(user, creditCard, orderLines2);

            List<Order> foundOrders = new List<Order>();
            foundOrders = TestUtil.orderDao.FindByUserId(user.id,0,10);

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

            foundOrders = TestUtil.orderDao.FindByUserId(NON_EXISTENT_ID_USER, 0, 10);

            Assert.AreEqual(0, foundOrders.Count);

            Order foundOrder = new Order();
            foundOrder = TestUtil.orderDao.Find(order1.id);

            Assert.AreEqual(order1.orderDate, foundOrder.orderDate);
            Assert.AreEqual(order1.totalPrice, foundOrder.totalPrice);
            Assert.AreEqual(order1.userId, foundOrder.userId);
            Assert.AreEqual(order1.totalPrice, foundOrder.totalPrice);
            Assert.AreEqual(order1.description, foundOrder.description);
        }
    }
}
