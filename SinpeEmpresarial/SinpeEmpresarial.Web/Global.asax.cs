using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SinpeEmpresarial.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SinpeEmpresarial.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            try
            {
                var context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Crear rol Administrador si no existe
                if (!roleManager.RoleExists("Administrador"))
                {
                    var role = new IdentityRole("Administrador");
                    roleManager.Create(role);
                }

                // Crear rol Cajero si no existe
                if (!roleManager.RoleExists("Cajero"))
                {
                    var role = new IdentityRole("Cajero");
                    roleManager.Create(role);
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                System.Diagnostics.Debug.WriteLine("Error al crear roles: " + ex.Message);
                // Don't throw the exception to prevent application crash
            }
        }
    }

}
