using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class SinpeRepository : ISinpeRepository
    {
        private readonly SinpeDbContext _context;
        public SinpeRepository(SinpeDbContext context)
        {
            _context = context;
        }
        public Comercio GetById(int id)
        {
            return _context.Comercios.FirstOrDefault(c => c.IdComercio == id);
        }
        public List<Comercio> GetAll()
        {
            return _context.Comercios.ToList();
        }
        public void Add(Comercio comercio)
        {
            _context.Comercios.Add(comercio);
            _context.SaveChanges();
        }
        public void Update(Comercio comercio)
        {
            _context.Entry(comercio).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
