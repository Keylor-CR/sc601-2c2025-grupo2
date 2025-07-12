using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos.ConfiguracionComercio
{
    public class ConfiguracionComercioEditDto
    {
        [Required(ErrorMessage = "El id de configuracion es requerido")]
        public int IdConfiguracion { get; set; }
        [Required(ErrorMessage = "El ID del comercio es requerido")]
        public int IdComercio { get; set; }
        [Required(ErrorMessage = "El tipo de configuración es requerida")]
        [Display(Name = "Tipo de configuración")]
        public int TipoConfiguracion { get; set; }
        [Display(Name = "Comisión")]
        [Required(ErrorMessage = "La comisión es requerida")]
        public int Comision { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
    }
}
