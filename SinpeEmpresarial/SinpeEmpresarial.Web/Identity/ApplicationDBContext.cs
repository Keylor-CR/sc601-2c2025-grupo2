using Microsoft.AspNet.Identity.EntityFramework;
using SinpeEmpresarial.Web.Models;

namespace SinpeEmpresarial.Web.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=SinpeDbContext") // Usamos tu misma cadena de conexión
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
