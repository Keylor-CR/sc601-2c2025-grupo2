﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinpeEmpresarial.Domain.Entities
{
    [Table("SINPES")]
    public class Sinpe
    {
        [Key]
        public int IdSinpe { get; set; }
        public string TelefonoOrigen { get; set; }
        public string NombreOrigen { get; set; }
        public string TelefonoDestino { get; set; }
        public string NombreDestino { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
