using System;

public class UsuarioDetailDto
{
    public int IdUsuario { get; set; }
    public int IdComercio { get; set; }
    public string Nombres { get; set; }
    public string PrimerApellido { get; set; }
    public string SegundoApellido { get; set; }
    public string Identificacion { get; set; }
    public string CorreoElectronico { get; set; }
    public DateTime FechaDeRegistro { get; set; }
    public DateTime? FechaDeModificacion { get; set; }
    public bool Estado { get; set; }
}
