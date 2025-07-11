using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.DTOs.Usuario
{
    public class EditUsuarioDto
    {
        [Required]
        public int IdUsuario { get; set; }

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

        public bool Estado { get; set; }
        [Required]
        public int IdComercio { get; set; }

    }
}
