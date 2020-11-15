using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;

using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

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
            kernel.Bind<IUserService>().To<UserService>();

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