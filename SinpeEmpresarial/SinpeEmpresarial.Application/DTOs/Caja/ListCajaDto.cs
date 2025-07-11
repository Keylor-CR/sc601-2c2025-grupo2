using System;

namespace SinpeEmpresarial.Application.Dtos.Caja
{
    public class ListCajaDto
    {
        public int IdCaja { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TelefonoSINPE { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
