using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IComercioRepository
    {

        Comercio GetById(int id);
        Comercio GetByIdentificacion(string identificacion);
        List<Comercio> GetAll();
        void Add(Comercio entity);
        void Update(Comercio entity);
    }
}
