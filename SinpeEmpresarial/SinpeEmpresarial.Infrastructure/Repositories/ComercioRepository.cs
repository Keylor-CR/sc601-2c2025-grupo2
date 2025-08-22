using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class ComercioRepository : IComercioRepository
    {
        private readonly SinpeDbContext _context;
        public ComercioRepository(SinpeDbContext context)
        {
            _context = context;
        }
        public Comercio GetById(int id)
        {
            return _context.COMERCIOS.FirstOrDefault(c => c.IdComercio == id);
        }

        public Comercio GetByTelefono(string telefono)
        {
            return _context.COMERCIOS.FirstOrDefault(c => c.Telefono == telefono);
        }

        public Comercio GetByIdentificacion(string identificacion)
        {
            return _context.COMERCIOS.FirstOrDefault(c => c.Identificacion == identificacion);
        }
        public List<Comercio> GetAll()
        {
            return _context.COMERCIOS.ToList();
        }
        public void Add(Comercio comercio)
        {
            _context.COMERCIOS.Add(comercio);
            _context.SaveChanges();
        }
        public void Update(Comercio comercio)
        {
            _context.Entry(comercio).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
