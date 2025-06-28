using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface IComercioRepository
    {

        Comercio GetById(int idComercio);
        Comercio GetByIdentificacion(string id);
        List<Comercio> GetAll();
        void Add(Comercio comercio);
        void Update(Comercio comercio);
    }
}
