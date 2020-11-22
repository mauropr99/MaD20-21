using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Ninject;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Test.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao.Tests
{
    [TestClass()]
    public class IOrderLineDaoEntityFrameworkTests
    {
        private static IKernel kernel;

        private const long NON_EXISTENT_ID_ORDER = -1;

      
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
        public void FindByOrderIdTest()
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

            List<OrderLine> foundOrderLines = new List<OrderLine>();
            foundOrderLines = TestUtil.orderLineDao.FindByOrderId(order1.id);

            Assert.AreEqual(2, foundOrderLines.Count);
            Assert.AreEqual(orderline1.id, foundOrderLines[0].id);
            Assert.AreEqual(orderline1.quantity, foundOrderLines[0].quantity);
            Assert.AreEqual(orderline1.productId, foundOrderLines[0].productId);
            Assert.AreEqual(orderline1.price, foundOrderLines[0].price);

            Assert.AreEqual(orderline2.id, foundOrderLines[1].id);
            Assert.AreEqual(orderline2.quantity, foundOrderLines[1].quantity);
            Assert.AreEqual(orderline2.productId, foundOrderLines[1].productId);
            Assert.AreEqual(orderline2.price, foundOrderLines[1].price);


            foundOrderLines = TestUtil.orderLineDao.FindByOrderId(NON_EXISTENT_ID_ORDER);

            Assert.AreEqual(0, foundOrderLines.Count);

            OrderLine foundOrderLine = new OrderLine();
            foundOrderLine = TestUtil.orderLineDao.Find(orderline1.id);

            Assert.AreEqual(orderline1.id, foundOrderLine.id);
            Assert.AreEqual(orderline1.quantity, foundOrderLine.quantity);
            Assert.AreEqual(orderline1.productId, foundOrderLine.productId);
            Assert.AreEqual(orderline1.price, foundOrderLine.price);
        }
    }
}
