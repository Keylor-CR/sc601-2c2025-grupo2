using System.Collections.Generic;
using SinpeEmpresarial.Application.DTOs.Sinpe;
using SinpeEmpresarial.Shared.Models;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface ISinpeService
    {
        List<ListSinpeDto> GetByCajaTelefono(string telefonoDestino);
        ResponseModel RegisterSinpe(SinpeCreateDto dto);
    }
}
