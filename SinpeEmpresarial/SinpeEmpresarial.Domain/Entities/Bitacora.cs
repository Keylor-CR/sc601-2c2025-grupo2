using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinpeEmpresarial.Domain.Entities
{
    [Table("BITACORA_EVENTOS")]
    public class Bitacora
    {
        [Key]
        public int IdEvento { get; set; }
        public string TablaDeEvento { get; set; }
        public string TipoDeEvento { get; set; }
        public DateTime FechaDeEvento { get; set; }
        public string DescripcionDeEvento { get; set; }
        public string StackTrace { get; set; }
        public string DatosAnteriores { get; set; }
        public string DatosPosteriores { get; set; }
    }
}
