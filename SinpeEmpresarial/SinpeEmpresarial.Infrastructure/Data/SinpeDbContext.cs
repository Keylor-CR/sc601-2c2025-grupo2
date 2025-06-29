using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Entities;
using System.Data.Entity;

namespace SinpeEmpresarial.Infrastructure.Data
{
    public class SinpeDbContext : DbContext
    {
        public SinpeDbContext() : base("name=SinpeDbContext")
        {
            Database.SetInitializer<SinpeDbContext>(null);
        }

        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Sinpe> Sinpes { get; set; }
        public DbSet<Bitacora> BITACORA_EVENTOS { get; set; }


    }
}
