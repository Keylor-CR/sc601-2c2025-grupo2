using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinpeEmpresarial.Domain.Entities
{
    [Table("CAJAS")]
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TelefonoSINPE { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; } 
    }
}
