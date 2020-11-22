﻿using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Tests
{
    [TestClass]
    public class ICommentServiceTest
    {

        private static IKernel kernel;
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
            TestUtil.commentDao = kernel.Get<ICommentDao>();
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
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                Language language = TestUtil.CreateExistentLanguage();

                long userId = userService.SingUpUser(login, password,
                       new UserDetails(name, lastName, email, language.name,language.country, address));
                User user = TestUtil.userDao.Find(userId);
                CreditCard creditCard = TestUtil.CreateCreditCard(user);

                Category category1 = TestUtil.CreateCategory("Ordenadores");
                Computer product1 = TestUtil.CreateComputer(category1, "Msi GL 62 6QD", 3, "Msi");
                List<string> labels = new List<string>
                {
                    "Ganga",
                    "Oferta",
                    "Chollazo"
                };

                string text = "Muy buen ordenador y a buen precio. Funcionan todos los juegos a calidad máxima, muy fluidos y sin apenas calentarse el aparato.";
                Comment comment = commentService.NewComment(user.id, product1.id, text, labels);

                text = "Es una bestia de portatil gaming , los juegos se ven genial y se inician en un momento , no se calienta y encima no es tan pesado .";

                List<string> labels2 = new List<String>
                {
                    "Irresistible",
                    "Chollazo",
                    "Ganga"
                };
                comment = commentService.UpdateComment(comment.id,text, labels2);

                Assert.AreNotEqual(labels2, comment.Labels);

                var foundComment = TestUtil.commentDao.Find(comment.id);

                Assert.AreEqual(comment.id, foundComment.id);
                Assert.AreEqual(text, foundComment.text);
                Assert.AreEqual(userId, foundComment.userId);
                Assert.AreEqual(labels2, foundComment.Labels);



            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void InstanceNotFoundExceptionTest()
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
                List<string> labels = new List<String>();
                labels.Add("Ganga");
                labels.Add("Oferta");
                labels.Add("Chollazo");

                string text = "Muy buen ordenado y a buen precio. Funcionan todos los juegos a calidad máxima, muy fluidos y sin apenas calentarse el aparato.";
                Comment comment = commentService.NewComment(userId, product1.id, text, labels);

                text = "Es una bestia de portatil gaming , los juegos se ven genial y se inician en un momento , no se calienta y encima no es tan pesado .";

                List<string> labels2 = new List<String>();
                labels.Add("Irresistible");
                labels.Add("Chollazo");
                labels.Add("Ganga");
                comment = commentService.UpdateComment(comment.id, text, labels);

                TestUtil.labelDao.FindByLabelName("oferta");

            }
        }

    }
}
