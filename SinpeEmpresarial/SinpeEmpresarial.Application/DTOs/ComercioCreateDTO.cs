using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs
{
    public class ComercioCreateDTO
    {
        [Required]
        public string Identificacion { get; set; }
        [Required]
        public int TipoIdentificacion { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int TipoDeComercio { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required, EmailAddress]
        public string CorreoElectronico { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
