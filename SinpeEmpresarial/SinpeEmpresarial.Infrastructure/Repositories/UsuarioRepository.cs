using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SinpeDbContext _context;

    public UsuarioRepository(SinpeDbContext context)
    {
        _context = context;
    }

    public void Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<Usuario> GetAll()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario GetById(int id)
    {
        return _context.Usuarios.Find(id);
    }

    public Usuario GetByIdentificacion(string identificacion)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Identificacion == identificacion);
    }

    public void Update(Usuario usuario)
    {
        _context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
        _context.SaveChanges();
    }
}
