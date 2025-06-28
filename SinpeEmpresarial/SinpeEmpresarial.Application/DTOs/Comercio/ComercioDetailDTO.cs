using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs
{
    public class ComercioDetailDTO
    {
        public string Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        public string TipoIdentificacionString { get; set; }
        public string Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        public string TipoDeComercioString { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        public string EstadoString { get; set; }
    }
}
