using SinpeEmpresarial.Domain.Entities;
using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface ISinpeRepository
    {
        Sinpe GetById(int id);
        List<Sinpe> GetAll();
        void Add(Sinpe entity);
        List<Sinpe> GetByTelefonoDestino(string telefono);
    }
}