using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao.Tests
{
    [TestClass()]
    public class ICommentDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
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

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_CATEGORY_NAME = "non_existent_cat";

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
            commentDao = kernel.Get<ICommentDao>();
            TestUtil.labelDao = kernel.Get<ILabelDao>();


            commentService = kernel.Get<ICommentService>();
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
        public void FindByProductIdTest()
        {
            using (var scope = new TransactionScope())
            {
                int startIndex = 0, count = 5;
                Language language = TestUtil.CreateExistentLanguage();

                long userId = userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name, language.country));
                User user = TestUtil.userDao.Find(userId);
                CreditCard creditCard = TestUtil.CreateCreditCard();

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");

                List<string> labels = new List<string>
                {
                    "Ganga",
                    "Oferta",
                    "Chollazo"
                };

                string text = "Muy buen ordenador y a buen precio. Funcionan todos los juegos a calidad máxima, muy fluidos y sin apenas calentarse el aparato.";
                var comment1 = commentService.NewComment(user.id, product1.id, text, labels);

                text = "Es una bestia de portatil gaming , los juegos se ven genial y se inician en un momento , no se calienta y encima no es tan pesado .";

                List<string> labels2 = new List<String>
                {
                    "Irresistible",
                    "Chollazo",
                    "Ganga"
                };
                var comment2 = commentService.NewComment(user.id, product1.id, text, labels2);

                List<Comment> comments = commentDao.FindCommentsByProductId(product1.id, startIndex, count);

                Assert.AreEqual(2, comments.Count);
                Assert.AreEqual(comment1.id, comments[0].id);
                Assert.AreEqual(comment2.id, comments[1].id);

            }
        }

        [TestMethod()]
        public void AddLabel()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

                long userId = userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name, language.country));
                CreditCard creditCard = TestUtil.CreateCreditCard();

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");

                Label label = new Label()
                {
                    lab = "Ganga",
                    timesUsed = 0
                };

                List<string> labels = new List<String>
                {
                    "Irresistible",
                    "Chollazo",
                };
                string text = "Muy buen ordenador y a buen precio. Funcionan todos los juegos a calidad máxima, muy fluidos y sin apenas calentarse el aparato.";
                var comment1 = commentService.NewComment(userId, product1.id, text, labels);

                commentDao.AddLabel(label, comment1.id);

                Assert.AreEqual(label.lab, TestUtil.labelDao.FindByLabelName(label.lab).lab);
            }
        }
    }
}

