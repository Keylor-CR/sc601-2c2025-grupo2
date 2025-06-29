using SinpeEmpresarial.Domain.Entities;
using System.Collections.Generic;

public interface ISinpeRepository
{
    Sinpe GetById(int id);
    List<Sinpe> GetAll();
    void Add(Sinpe sinpe);
    List<Sinpe> GetByTelefonoDestino(string telefono);
}
