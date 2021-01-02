using System.Configuration;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;


namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* BookDao */
            kernel.Bind<IBookDao>().
                To<BookDaoEntityFramework>();

            /* ComputerDao */
            kernel.Bind<IComputerDao>().
                To<ComputerDaoEntityFramework>();

            /* OrderDao */
            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            /* OrderLineDao */
            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            /* UserDao */
            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            /* LanguageDao */
            kernel.Bind<ILanguageDao>().
                To<LanguageDaoEntityFramework>();

            /* ProductDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            /* CreditCardDao */
            kernel.Bind<ICreditCardDao>().
                To<CreditCardDaoEntityFramework>();

            /* ProductDao */
            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* ProductService */
            kernel.Bind<IProductService>().
                To<ProductService>();

            /* ShoppingService */
            kernel.Bind<IShoppingService>().
                To<ShoppingService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["PracticaMaDEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}