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
            return _context.CAJAS.FirstOrDefault(c => c.IdCaja == id);
        }

        public List<Caja> GetAll() =>
            _context.CAJAS.ToList();

        public void Add(Caja caja)
        {
            _context.CAJAS.Add(caja);
            _context.SaveChanges();
        }

        public void Update(Caja caja)
        {
            _context.Entry(caja).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Caja GetByNombre(string nombre, int idComercio) =>
            _context.CAJAS.FirstOrDefault(c => c.Nombre == nombre && c.IdComercio == idComercio);

        public Caja GetByTelefono(string telefono) =>
            _context.CAJAS.FirstOrDefault(c => c.TelefonoSINPE == telefono && c.Estado);

        public List<Caja> GetByComercio(int idComercio)
        {
            return _context.CAJAS
                .Where(c => c.IdComercio == idComercio)
                .ToList();
        }

        public Caja GetCajaByPhone(string telefono)
        {
            return _context.CAJAS.FirstOrDefault(c => c.TelefonoSINPE == telefono);
        }
    }
}
