using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.DTOs.Caja
{
    public class EditCajaDto
    {
        
        public int IdCaja { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Telefono SINPE")]
        public string TelefonoSINPE { get; set; }
        public bool Estado { get; set; }
    }
}
