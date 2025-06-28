using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly SinpeDbContext _context;

        public BitacoraRepository(SinpeDbContext context)
        {
            _context = context;
        }

        public Bitacora GetById(int id)
        {
            return _context.Bitacoras.FirstOrDefault(b => b.IdEvento == id);
        }

        public List<Bitacora> GetAll()
        {
            return _context.Bitacoras.OrderByDescending(b => b.FechaDeEvento).ToList();
        }

        public void Add(Bitacora bitacora)
        {
            _context.Bitacoras.Add(bitacora);
            _context.SaveChanges();
        }
    }
}
