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
        [Required(ErrorMessage = "La identificacion es requerida")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El tipo de identificacion es requerido")]
        public int TipoIdentificacion { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El tipo de comercio es requerido")]
        public int TipoDeComercio { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electronico no es valido")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "La direccion es requerida")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
    }
}
