using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs
{
    public class ComercioDetailDTO
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
        public string Telefono { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Estado")]
        public string EstadoString { get; set; }
    }
}
