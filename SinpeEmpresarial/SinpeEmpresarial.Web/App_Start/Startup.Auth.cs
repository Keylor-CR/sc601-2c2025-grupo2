using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SinpeEmpresarial.Web;
using SinpeEmpresarial.Web.Identity;
using SinpeEmpresarial.Web.Models;
using System;

[assembly: OwinStartup(typeof(SinpeEmpresarial.Web.Startup))]

namespace SinpeEmpresarial.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 👇 OWIN contexts para Identity
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 👇 Configuración de autenticación con cookies
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"), // Ruta al login
                Provider = new CookieAuthenticationProvider
                {
                    // Seguridad extra para validar el usuario en cada request
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<
                        ApplicationUserManager,
                        ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) =>
                            manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });

            // 👇 Autenticación externa si usás Google, Facebook, etc. (opcional)
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
