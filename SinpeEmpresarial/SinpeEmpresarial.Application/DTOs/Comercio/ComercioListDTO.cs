using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs
{
    public class ComercioListDTO
    {
        public int IdComercio { get; set; }
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacionString { get; set; }
        public string Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        [Display(Name = "Tipo de Comercio")]
        public string TipoDeComercioString { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Estado")]
        public string EstadoString { get; set; }
    }
}
