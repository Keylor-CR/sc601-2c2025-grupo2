using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class ConfiguracionComercioRepository : IConfiguracionComercioRepository
    {
        private readonly SinpeDbContext _context;
        public ConfiguracionComercioRepository(SinpeDbContext context)
        {
            _context = context;
        }
        public ConfiguracionComercio GetById(int id)
        {
            return _context.CONFIGURACIONES_COMERCIOS.FirstOrDefault(c => c.IdComercio == id);
        }
        public ConfiguracionComercio GetByComercioId(int idComercio)
        {
            return _context.CONFIGURACIONES_COMERCIOS.FirstOrDefault(c => c.IdComercio == idComercio);
        }
        public List<ConfiguracionComercio> GetAll()
        {
            return _context.CONFIGURACIONES_COMERCIOS.ToList();
        }
        public void Add(ConfiguracionComercio configuracioncomercio)
        {
            _context.CONFIGURACIONES_COMERCIOS.Add(configuracioncomercio);
            _context.SaveChanges();
        }
        public void Update(ConfiguracionComercio configuracioncomercio)
        {
            _context.Entry(configuracioncomercio).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
