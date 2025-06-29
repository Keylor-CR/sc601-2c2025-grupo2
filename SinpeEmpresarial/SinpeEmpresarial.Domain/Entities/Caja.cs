using System;
using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Domain.Entities
{
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TelefonoSINPE { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }

        public void Edit(string nombre, string descripcion, string telefonoSINPE, bool estado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            TelefonoSINPE = telefonoSINPE;
            Estado = estado;
            FechaDeModificacion = DateTime.Now;
        }
    }
}
