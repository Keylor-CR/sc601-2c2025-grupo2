using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Infrastructure.Repositories
{
    public class ReportesMensualesRepository : IReportesMensualesRepository
    {
        private readonly SinpeDbContext _context;

        public ReportesMensualesRepository(SinpeDbContext context)
        {
            _context = context;
        }

        public List<ReportesMensuales> GetAll()
        {
            return _context.ReportesMensuales.ToList();
        }

        public ReportesMensuales GetById(int id)
        {
            return _context.ReportesMensuales.FirstOrDefault(r => r.IdReporte == id);
        }

        public ReportesMensuales GetByComercioYFecha(int idComercio, DateTime fecha)
        {
            return _context.ReportesMensuales.FirstOrDefault(r => r.IdComercio == idComercio && 
                r.FechaDelReporte.Month == fecha.Month && r.FechaDelReporte.Year == fecha.Year);
        }

        public void Add(ReportesMensuales reporte)
        {
            _context.ReportesMensuales.Add(reporte);
            _context.SaveChanges();
        }

        public void Update(ReportesMensuales reporte)
        {
            _context.Entry(reporte).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reporte = GetById(id);
            if (reporte != null)
            {
                _context.ReportesMensuales.Remove(reporte);
                _context.SaveChanges();
            }
        }
    }
}
