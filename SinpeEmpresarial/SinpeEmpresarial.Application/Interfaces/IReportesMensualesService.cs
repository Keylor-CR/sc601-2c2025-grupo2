using SinpeEmpresarial.Application.DTOs.ReportesMensuales;
using System.Collections.Generic;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IReportesMensualesService
    {
        List<ListReportesMensualesDto> GetAllReportes();
        void GenerarReportesMensuales();
    }
}
