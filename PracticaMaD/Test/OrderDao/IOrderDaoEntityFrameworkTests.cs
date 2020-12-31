using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

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
            TestUtil.computerDao = kernel.Get<IComputerDao>();
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
        public void FindByUserIdTest()
        {
            using (var scope = new TransactionScope())
            {
                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Category category2 = TestUtil.CreateCategory("Libros");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                Computer product2 = TestUtil.CreateComputer(category1, "ACER 3x2600", 2.5m, "Acer");
                Book product3 = TestUtil.CreateBook(category2, "El quijote Nueva edición", 3.5m, "El quijote");
                OrderLine orderline1 = TestUtil.CreateOrderLine(product1);
                OrderLine orderline2 = TestUtil.CreateOrderLine(product2);
                OrderLine orderline3 = TestUtil.CreateOrderLine(product3);
                List<OrderLine> orderLines = new List<OrderLine>
            {
                orderline1,
                orderline2
            };
                Language language = TestUtil.CreateExistentLanguage();
                User user = TestUtil.CreateExistentUser(language);
                CreditCard creditCard = TestUtil.CreateCreditCard(user);
                Order order1 = TestUtil.CreateOrder(user, creditCard, orderLines);
                List<OrderLine> orderLines2 = new List<OrderLine>();
                orderLines.Add(orderline3);
                Order order2 = TestUtil.CreateOrder(user, creditCard, orderLines2);

                List<Order> foundOrders = new List<Order>();
                foundOrders = TestUtil.orderDao.FindByUserId(user.id, 0, 10);

                Assert.AreEqual(2, foundOrders.Count);
                Assert.AreEqual(order1.orderDate, foundOrders[0].orderDate);
                Assert.AreEqual(order1.totalPrice, foundOrders[0].totalPrice);
                Assert.AreEqual(order1.userId, foundOrders[0].userId);
                Assert.AreEqual(order1.totalPrice, foundOrders[0].totalPrice);
                Assert.AreEqual(order1.description, foundOrders[0].description); Assert.AreEqual(2, foundOrders.Count);
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
}
