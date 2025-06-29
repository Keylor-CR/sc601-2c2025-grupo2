using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class SinpeRepository : ISinpeRepository
    {
        private readonly SinpeDbContext _context;
        public SinpeRepository(SinpeDbContext context)
        {
            _context = context;
        }
        public Sinpe GetById(int id)
        {
            return _context.Sinpes.FirstOrDefault(c => c.IdSinpe == id);
        }
        public List<Sinpe> GetAll()
        {
            return _context.Sinpes.ToList();
        }
        public void Add(Sinpe sinpe)
        {
            _context.Sinpes.Add(sinpe);
            _context.SaveChanges();
        }
        public List<Sinpe> GetByTelefonoDestino(string telefono)
        {
            return _context.Sinpes
                .Where(s => s.TelefonoDestino == telefono)
                .ToList();
        }
    }
}
