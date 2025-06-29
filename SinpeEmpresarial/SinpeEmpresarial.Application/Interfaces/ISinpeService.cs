using System.Collections.Generic;
using SinpeEmpresarial.Application.DTOs.Sinpe;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface ISinpeService
    {
        List<ListSinpeDto> GetByCajaTelefono(string telefonoDestino);
    }
}
