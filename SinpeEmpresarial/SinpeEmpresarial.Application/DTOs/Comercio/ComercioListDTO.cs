using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs
{
    public class ComercioListDTO
    {
        public int IdComercio { get; set; }
        public string Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        public string TipoIdentificacionString { get; set; }
        public string Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        public string TipoDeComercioString { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Estado { get; set; }
        public string EstadoString { get; set; }
    }
}
