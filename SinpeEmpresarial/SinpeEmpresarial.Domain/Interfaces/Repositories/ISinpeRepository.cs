using SinpeEmpresarial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface ISinpeRepository
    {
        Sinpe GetById(int id);
        List<Sinpe> GetAll();
        void Add(Sinpe sinpe);
    }
}
