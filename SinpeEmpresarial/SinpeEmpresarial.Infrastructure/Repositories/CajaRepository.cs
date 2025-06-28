using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class CajaRepository : ICajaRepository
    {
        private readonly SinpeDbContext _context;

        public CajaRepository(SinpeDbContext context)
        {
            _context = context;
        }

        public Caja GetById(int id)
        {
            return _context.Cajas.FirstOrDefault(c => c.IdCaja == id);
        }

        public List<Caja> GetAll()
        {
            return _context.Cajas.ToList();
        }

        public void Add(Caja caja)
        {
            _context.Cajas.Add(caja);
            _context.SaveChanges();
        }

        public void Update(Caja caja)
        {
            _context.Entry(caja).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
