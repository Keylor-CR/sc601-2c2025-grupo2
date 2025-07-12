using SinpeEmpresarial.Domain.Entities;
using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IConfiguracionComercioRepository
    {
        List<ConfiguracionComercio> GetAll();
        ConfiguracionComercio GetById(int id);
        ConfiguracionComercio GetByComercioId(int idComercio);
        void Add(ConfiguracionComercio entity);
        void Update(ConfiguracionComercio entity);
    }
}