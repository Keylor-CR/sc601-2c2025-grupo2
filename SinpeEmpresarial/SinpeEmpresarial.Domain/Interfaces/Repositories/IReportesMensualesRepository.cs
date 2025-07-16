using SinpeEmpresarial.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IReportesMensualesRepository
    {
        List<ReportesMensuales> GetAll();
        ReportesMensuales GetById(int id);
        ReportesMensuales GetByComercioYFecha(int idComercio, DateTime fecha);
        void Add(ReportesMensuales reporte);
        void Update(ReportesMensuales reporte);
        void Delete(int id);
    }
}
