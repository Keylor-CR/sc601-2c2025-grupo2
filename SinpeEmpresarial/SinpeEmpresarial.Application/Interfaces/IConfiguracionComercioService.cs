using SinpeEmpresarial.Application.Dtos.ConfiguracionComercio;
using System.Collections.Generic;
namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IConfiguracionComercioService
    {
        void RegisterConfiguracionComercio(ConfiguracionComercioCreateDto dto);
        void EditConfiguracionComercio(ConfiguracionComercioEditDto dto);
        List<ConfiguracionComercioListDto> GetAllConfiguraciones();
        ConfiguracionComercioDetailDto GetConfiguracionById(int id);
        ConfiguracionComercioDetailDto GetConfiguracionByComercio(int idComercio);

    }

}
