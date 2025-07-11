using System.Collections.Generic;
using SinpeEmpresarial.Domain.Entities;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface ICajaRepository
    {
        Caja GetById(int id);
        List<Caja> GetAll();
        Caja GetByNombre(string nombre, int idComercio);
        Caja GetByTelefono(string telefono);
        List<Caja> GetByComercio(int idComercio);
        void Add(Caja entity);
        void Update(Caja entity);
    }
}
