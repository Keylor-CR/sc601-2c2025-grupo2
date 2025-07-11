using System;

namespace SinpeEmpresarial.Application.Dtos.Sinpe
{
    public class ListSinpeDto
    {
        public string TelefonoOrigen { get; set; }
        public string NombreOrigen { get; set; }
        public string TelefonoDestino { get; set; }
        public string NombreDestino { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }
}
