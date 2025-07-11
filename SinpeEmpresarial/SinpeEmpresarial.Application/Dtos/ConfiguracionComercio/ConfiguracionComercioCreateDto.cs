using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos.ConfiguracionComercio
{
    public class ConfiguracionComercioCreateDto
    {
        [Required(ErrorMessage = "El Comercio es requerido")]
        public int IdComercio { get; set; }
        [Required(ErrorMessage = "El tipo de configuracion es requerido")]
        [Display(Name = "Tipo de Configuracion")]
        public int TipoConfiguracion { get; set; }
        [Required(ErrorMessage = "La comisión es requerida")]
        [Display(Name = "Comisión")]
        public int Comision { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
    }
}
