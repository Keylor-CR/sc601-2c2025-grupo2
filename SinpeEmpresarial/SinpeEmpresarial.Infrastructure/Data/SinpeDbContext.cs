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

        public DbSet<Comercio> COMERCIOS { get; set; }
        public DbSet<Caja> CAJAS { get; set; }
        public DbSet<Sinpe> SINPES { get; set; }
        public DbSet<Bitacora> BITACORA_EVENTOS { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ConfiguracionComercio> CONFIGURACIONES_COMERCIOS { get; set; }


    }
}
