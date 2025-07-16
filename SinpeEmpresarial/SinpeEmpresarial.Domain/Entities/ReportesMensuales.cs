using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinpeEmpresarial.Domain.Entities
{
    [Table("REPORTES_MENSUALES")]
    public class ReportesMensuales
    {
        [Key]
        public int IdReporte { get; set; }
        public int IdComercio { get; set; }
        public int CantidadDeCajas { get; set; }
        public decimal MontoTotalRecaudado { get; set; }
        public int CantidadDeSINPES { get; set; }
        public decimal MontoTotalComision { get; set; }
        public DateTime FechaDelReporte { get; set; }
    }
}
