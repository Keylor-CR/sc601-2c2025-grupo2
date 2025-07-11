using System.Collections.Generic;
using SinpeEmpresarial.Application.Dtos.Sinpe;
using SinpeEmpresarial.Shared.Models;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface ISinpeService
    {
        List<ListSinpeDto> GetByCajaTelefono(string telefonoDestino);
        ResponseModel RegisterSinpe(SinpeCreateDto dto);
        List<ListSinpeDto> GetLast(int count);
    }
}
