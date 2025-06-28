using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Domain.Entities
{
    public class Caja
    {
        public int IdCaja { get; set; }
        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TelefonoSINPE { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
