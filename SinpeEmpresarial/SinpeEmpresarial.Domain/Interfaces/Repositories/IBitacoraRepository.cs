using SinpeEmpresarial.Domain.Entities;
using System.Collections.Generic;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IBitacoraRepository
    {
        void Add(Bitacora entity);
        List<Bitacora> GetAll();
    }
}
