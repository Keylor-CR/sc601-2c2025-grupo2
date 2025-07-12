using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.DTOs.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        public int IdComercio { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        [Required]
        public string SegundoApellido { get; set; }

        [Required]
        [StringLength(10)]
        public string Identificacion { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }
    }
}
