using System;

namespace SinpeEmpresarial.Application.DTOs.ReportesMensuales
{
    public class ListReportesMensualesDto
    {
        public int IdReporte { get; set; }
        public int IdComercio { get; set; }
        public string NombreComercio { get; set; }
        public int CantidadDeCajas { get; set; }
        public decimal MontoTotalRecaudado { get; set; }
        public int CantidadDeSINPES { get; set; }
        public decimal MontoTotalComision { get; set; }
        public DateTime FechaDelReporte { get; set; }
    }
}
