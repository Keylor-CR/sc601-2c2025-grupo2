using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IComercioRepository
    {

        Comercio GetById(int id);
        Comercio GetByTelefono(string telefono);
        Comercio GetByIdentificacion(string identificacion);
        List<Comercio> GetAll();
        void Add(Comercio entity);
        void Update(Comercio entity);
    }
}
