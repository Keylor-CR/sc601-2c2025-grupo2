using SinpeEmpresarial.Domain.Entities;
using System.Collections.Generic;

public interface IUsuarioRepository
{
    Usuario GetById(int id);
    Usuario GetByIdentificacion(string identificacion);
    List<Usuario> GetAll();
    void Add(Usuario usuario);
    void Update(Usuario usuario);
}
