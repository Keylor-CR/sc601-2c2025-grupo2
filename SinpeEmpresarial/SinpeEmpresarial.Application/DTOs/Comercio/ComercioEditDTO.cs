﻿using System.ComponentModel.DataAnnotations;

namespace SinpeEmpresarial.Application.Dtos
{
    public class ComercioEditDto
    {
        [Required(ErrorMessage = "El ID del comercio es requerido")]
        public int IdComercio { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de comercio es requerido")]
        [Display(Name = "Tipo de Comercio")]
        public int TipoDeComercio { get; set; }

        [Required(ErrorMessage = "El telefono es requerido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electronico no es valido")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La direccion es requerida")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
    }
}
