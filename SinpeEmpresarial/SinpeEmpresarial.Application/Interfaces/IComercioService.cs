using SinpeEmpresarial.Application.Dtos;
using System.Collections.Generic;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IComercioService
    {
        ComercioDetailDto GetComercioById(int id);
        ComercioDetailDto GetComercioByIdentificacion(string id);
        List<ComercioListDto> GetAllComercios();
        void RegisterComercio(ComercioCreateDto dto);
        void EditComercio(ComercioEditDto dto);
    }
}
