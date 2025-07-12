using System;
using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos.ConfiguracionComercio
{
    public class ConfiguracionComercioDetailDto
    {
        public int IdConfiguracion { get; set; }
        public int IdComercio { get; set; }
        [Display(Name = "Nombre del Comercio")]
        public string NombreComercio { get; set; }
        public int TipoConfiguracion { get; set; }
        [Display(Name = "Tipo de Configuración")]
        public string TipoConfiguracionString { get; set; }
        [Display(Name = "Comisión")]
        public int Comision { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Estado")]
        public string EstadoString { get; set; }
    }
}
