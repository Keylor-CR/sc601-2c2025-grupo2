using System.Collections.Generic;

public interface IUsuarioService
{
    List<ListUsuarioDto> GetAll();
    UsuarioDetailDto GetById(int id);
    void Register(CreateUsuarioDto dto);
    void Edit(EditUsuarioDto dto);
}
