using System;
using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public int IdComercio { get; set; } // FK to Comercio
        public Guid? IdNetUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(100)]
        public string SegundoApellido { get; set; }

        [Required]
        [MaxLength(10)]
        public string Identificacion { get; set; }

        [Required]
        [MaxLength(200)]
        public string CorreoElectronico { get; set; }

        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }

        public bool Estado { get; set; } // Activo (1), Inactivo (0)

        public void Edit(string nombres, string primerApellido, string segundoApellido, string identificacion, string correo, bool estado)
        {
            Nombres = nombres;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Identificacion = identificacion;
            CorreoElectronico = correo;
            Estado = estado;
            FechaDeModificacion = DateTime.Now;
        }
    }
}
