using SinpeEmpresarial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Domain.Interfaces.Repositories
{
    public interface ICajaRepository
    {
        Caja GetById(int id);
        List<Caja> GetAll();
        void Add(Caja sinpe);
        void Update(Caja sinpe);
    }
}
