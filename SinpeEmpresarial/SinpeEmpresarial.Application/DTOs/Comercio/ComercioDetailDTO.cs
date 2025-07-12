using System;
using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos
{
    public class ComercioDetailDto
    {
        public int IdComercio { get; set; }
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacionString { get; set; }
        public string Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        [Display(Name = "Tipo de Comercio")]
        public string TipoDeComercioString { get; set; }
        public string Telefono { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Estado")]
        public string EstadoString { get; set; }
    }
}
