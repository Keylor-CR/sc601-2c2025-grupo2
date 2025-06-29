[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SinpeEmpresarial.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SinpeEmpresarial.Web.App_Start.NinjectWebCommon), "Stop")]

namespace SinpeEmpresarial.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;
    using SinpeEmpresarial.Application.Interfaces;
    using SinpeEmpresarial.Application.Services;
    using SinpeEmpresarial.Domain.Interfaces.Repositories;
    using SinpeEmpresarial.Infrastructure.Repositories;
    using SinpeEmpresarial.Infrastructure.Data;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Comercio
            kernel.Bind<IComercioRepository>().To<ComercioRepository>();
            kernel.Bind<IComercioService>().To<ComercioService>();

            // Caja
            kernel.Bind<ICajaRepository>().To<CajaRepository>();
            kernel.Bind<ICajaService>().To<CajaService>();

            //sinpe
            kernel.Bind<ISinpeRepository>().To<SinpeRepository>();
            kernel.Bind<ISinpeService>().To<SinpeService>();

            // EF DbContext
            kernel.Bind<SinpeDbContext>().ToSelf().InRequestScope();
        }
    }
}