﻿using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test.Util;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao.Tests
{
    [TestClass()]
    public class ILabelDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ICommentService commentService;


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
            TestUtil.categoryDao = kernel.Get<ICategoryDao>();
            TestUtil.bookDao = kernel.Get<IBookDao>();
            TestUtil.labelDao = kernel.Get<ILabelDao>();
            TestUtil.commentDao = kernel.Get<ICommentDao>();
            TestUtil.userDao = kernel.Get<IUserDao>();

            commentService = kernel.Get<ICommentService>();
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
        public void ExistsByLabelNameTest()
        {
            Category category1 = TestUtil.CreateCategory("Books");
            Book product1 = TestUtil.CreateBook(category1, "El Quijote Nueva edición", 3.5m, "El Quijote");
        }

        [TestMethod()]
        public void FindMostUsedLabelTest()
        {
            int quantity = 3;

            Category category1 = TestUtil.CreateCategory("Books");
            Book product1 = TestUtil.CreateBook(category1, "El Quijote Nueva edición", 3.5m, "El Quijote");
            Book product2 = TestUtil.CreateBook(category1, "El Quijote Edición de bolsillo", 3.5m, "El Quijote");
            Book product3 = TestUtil.CreateBook(category1, "El Quijote Vieja edición", 3.5m, "El Quijote");
            Language language = TestUtil.CreateExistentLanguage();
            User user1 = TestUtil.CreateExistentUser(language);
            User user2= TestUtil.CreateExistentUser(language);
            Comment comment1 = TestUtil.CreateComment(user1,product1);
            Comment comment2 = TestUtil.CreateComment(user1,product2);
            Comment comment3 = TestUtil.CreateComment(user1,product3);
            Comment comment4 = TestUtil.CreateComment(user2,product3);

            List<string> labels1 = new List<string>
            {
                "Espectacular",
                "Fresco",
                "Alucinante",
                "Entretenido"
            };

            List<string> labels2 = new List<string>
            {
                "Espectacular",
                "Fresco",
                "Alucinante"
            };

            List<string> labels3 = new List<string>
            {
                "Espectacular",
                "Fresco",
            };

            List<string> labels4 = new List<string>
            {
                "fresco",
            };

     
            commentService.UpdateComment(user1.id, comment1.id, comment1.text, labels1);
            commentService.UpdateComment(user1.id, comment2.id, comment2.text, labels2);
            commentService.UpdateComment(user1.id, comment3.id, comment3.text, labels3);
            commentService.UpdateComment(user2.id, comment4.id, comment4.text, labels4);


            List<Label> mostUsedLabels = TestUtil.labelDao.FindMostUsedLabel(quantity);

            Assert.AreEqual(quantity, mostUsedLabels.Count);
            Assert.AreEqual("Fresco", mostUsedLabels[0].lab);
            Assert.AreEqual("Espectacular", mostUsedLabels[1].lab);
            Assert.AreEqual("Alucinante", mostUsedLabels[2].lab);

            List<string> labels5 = new List<string>
            {
                "Espectacular"
            };

            commentService.UpdateComment(user2.id, comment4.id, comment4.text, labels5);

            mostUsedLabels = TestUtil.labelDao.FindMostUsedLabel(quantity);

            Assert.AreEqual(quantity, mostUsedLabels.Count);
            Assert.AreEqual("Espectacular", mostUsedLabels[0].lab);
            Assert.AreEqual("Fresco", mostUsedLabels[1].lab);
            Assert.AreEqual("Alucinante", mostUsedLabels[2].lab);
        }


    }
}

