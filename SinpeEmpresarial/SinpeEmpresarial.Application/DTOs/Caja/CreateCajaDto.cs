using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.DTOs.Caja
{
    public class CreateCajaDto
    {
        [Required(ErrorMessage = "El ID del comercio es requerido")]
        [Display(Name = "Comercio")]
        public int IdComercio { get; set; }
        
        [Required(ErrorMessage = "El nombre de la caja es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }
        
        [StringLength(200, ErrorMessage = "La descripcion no puede exceder los 200 caracteres")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage = "El telefono SINPE es requerido")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "El telefono debe tener entre 8 y 10 digitos")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "El telefono debe contener solo digitos entre 8 y 10 caracteres")]
        [Display(Name = "Telefono SINPE")]
        public string TelefonoSINPE { get; set; }
    }
}
