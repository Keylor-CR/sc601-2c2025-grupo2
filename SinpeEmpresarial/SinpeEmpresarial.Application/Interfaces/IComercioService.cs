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
        ComercioDetailDTO GetById(int id);
        ComercioDetailDTO GetByIdentificacion(string id);
        List<ComercioListDTO> GetAll();
        void Register(ComercioCreateDTO dto);
        void Edit(ComercioEditDTO dto);
    }
}
