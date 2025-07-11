using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos
{
    public class ComercioCreateDto
    {
        [Required(ErrorMessage = "La identificacion es requerida")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El tipo de identificacion es requerido")]
        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacion { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El tipo de comercio es requerido")]
        [Display(Name = "Tipo de Comercio")]
        public int TipoDeComercio { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electronico no es valido")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "La direccion es requerida")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
    }
}
