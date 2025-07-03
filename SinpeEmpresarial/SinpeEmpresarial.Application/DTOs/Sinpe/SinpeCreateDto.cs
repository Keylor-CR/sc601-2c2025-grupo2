using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.DTOs.Sinpe
{
    public class SinpeCreateDto
    {
        [Required(ErrorMessage = "El telefono de origen es requerido")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "El telefono debe tener entre 8 y 10 digitos")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "El telefono debe contener solo digitos entre 8 y 10 caracteres")]
        public string TelefonoOrigen { get; set; }

        [Required(ErrorMessage = "El nombre de origen es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string NombreOrigen { get; set; }

        [Required(ErrorMessage = "El telefono destinatario es requerido")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "El telefono debe tener entre 8 y 10 digitos")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "El telefono debe contener solo digitos entre 8 y 10 caracteres")]
        public string TelefonoDestinatario { get; set; }

        [Required(ErrorMessage = "El nombre destinatario es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string NombreDestinatario { get; set; }

        [Required(ErrorMessage = "El monto es requerido")]
        [Range(1, 999999999, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [StringLength(50, ErrorMessage = "La descripcion no puede exceder los 50 caracteres")]
        public string Descripcion { get; set; }
    }
}
