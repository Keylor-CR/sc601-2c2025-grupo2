using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.DTOs.Bitacora
{
    public class BitacoraEventoDTO
    {
        [Display(Name="ID")]
        public int IdEvento { get; set; }
        [Display(Name="Tabla")]
        public string TablaDeEvento { get; set; }
        [Display(Name="Tipo")]
        public string TipoDeEvento { get; set; }
        [Display(Name="Fecha")]
        public DateTime FechaDeEvento { get; set; }
        [Display(Name="Descripcion")]
        public string DescripcionDeEvento { get; set; }
        [Display(Name="Traza")]
        public string StackTrace { get; set; }
        [Display(Name="Datos Anteriores")]
        public string DatosAnteriores { get; set; }
        [Display(Name="Datos Posteriores")]
        public string DatosPosteriores { get; set; }
    }
}
