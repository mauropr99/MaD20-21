using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region Option A : configuration via sourcecode

            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserDao>().To<UserDaoEntityFramework>();
            kernel.Bind<ILanguageDao>().To<LanguageDaoEntityFramework>();
            kernel.Bind<ILabelDao>().To<LabelDaoEntityFramework>();
            kernel.Bind<ICreditCardDao>().To<CreditCardDaoEntityFramework>();
            kernel.Bind<IOrderDao>().To<OrderDaoEntityFramework>();
            kernel.Bind<IOrderLineDao>().To<OrderLineDaoEntityFramework>();
            kernel.Bind<IProductDao>().To<ProductDaoEntityFramework>();
            kernel.Bind<IComputerDao>().To<ComputerDaoEntityFramework>();
            kernel.Bind<IBookDao>().To<BookDaoEntityFramework>();
            kernel.Bind<ICategoryDao>().To<CategoryDaoEntityFramework>();
            kernel.Bind<ICommentDao>().To<CommentDaoEntityFramework>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IShoppingService>().To<ShoppingService>();
            kernel.Bind<ICommentService>().To<CommentService>();




            string connectionString =
                ConfigurationManager.ConnectionStrings["practicamad_testEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion Option A : configuration via sourcecode

            #region Option B: configuration via external XML configuration file

            // The kernel should automatically load extensions at startup
            //NinjectSettings settings = new NinjectSettings() { LoadExtensions = false };
            //IKernel kernel = new StandardKernel(settings, new Ninject.Extensions.Xml.XmlExtensionModule());

            //kernel.Load("Ninject_Config.xml");

            #endregion Option B: configuration via external XML configuration file

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}