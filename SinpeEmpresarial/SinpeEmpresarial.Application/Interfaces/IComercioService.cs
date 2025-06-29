using SinpeEmpresarial.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IComercioService
    {
        ComercioDetailDTO GetComercioById(int id);
        ComercioDetailDTO GetComercioByIdentificacion(string id);
        List<ComercioListDTO> GetAllComercios();
        void RegisterComercio(ComercioCreateDTO dto);
        void EditComercio(ComercioEditDTO dto);
    }
}
