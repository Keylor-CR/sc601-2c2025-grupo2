using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void Add(Bitacora evento)
        {
            _context.BITACORA_EVENTOS.Add(evento);
            _context.SaveChanges();
        }

        public List<Bitacora> GetAll()
        {
            return _context.BITACORA_EVENTOS.OrderByDescending(b => b.FechaDeEvento).ToList();
        }
    }
}
