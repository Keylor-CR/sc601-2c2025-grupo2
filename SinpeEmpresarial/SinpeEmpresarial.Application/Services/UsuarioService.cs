using Newtonsoft.Json;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.DTOs.Usuario;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;
    private readonly IBitacoraService _bitacoraService;

    public UsuarioService(IUsuarioRepository repo, IBitacoraService bitacoraService)
    {
        _repo = repo;
        _bitacoraService = bitacoraService;
    }

    public List<ListUsuarioDto> GetAll()
    {
        return _repo.GetAll().Select(u => new ListUsuarioDto
        {
            IdUsuario = u.IdUsuario,
            Nombres = u.Nombres,
            PrimerApellido = u.PrimerApellido,
            SegundoApellido = u.SegundoApellido,
            Identificacion = u.Identificacion,
            CorreoElectronico = u.CorreoElectronico,
            FechaDeRegistro = u.FechaDeRegistro,
            FechaDeModificacion = u.FechaDeModificacion,
            Estado = u.Estado
        }).ToList();
    }

    public UsuarioDetailDto GetById(int id)
    {
        var u = _repo.GetById(id);
        if (u == null) return null;

        return new UsuarioDetailDto
        {
            IdUsuario = u.IdUsuario,
            IdComercio = u.IdComercio,
            Nombres = u.Nombres,
            PrimerApellido = u.PrimerApellido,
            SegundoApellido = u.SegundoApellido,
            Identificacion = u.Identificacion,
            CorreoElectronico = u.CorreoElectronico,
            FechaDeRegistro = u.FechaDeRegistro,
            FechaDeModificacion = u.FechaDeModificacion,
            Estado = u.Estado
        };
    }

    public void Register(CreateUsuarioDto dto)
    {
        if (_repo.GetByIdentificacion(dto.Identificacion) != null)
            throw new Exception("Ya existe un usuario con esa identificación");

        var u = new Usuario
        {
            IdComercio = dto.IdComercio,
            Nombres = dto.Nombres,
            PrimerApellido = dto.PrimerApellido,
            SegundoApellido = dto.SegundoApellido,
            Identificacion = dto.Identificacion,
            CorreoElectronico = dto.CorreoElectronico,
            FechaDeRegistro = DateTime.Now,
            Estado = true
        };

        _repo.Add(u);

        _bitacoraService.RegisterEvento(new BitacoraEventoDto
        {
            TablaDeEvento = "USUARIOS",
            TipoDeEvento = "Crear",
            DescripcionDeEvento = "Creacion de usuario",
            StackTrace = "",
            DatosAnteriores = null,
            DatosPosteriores = JsonConvert.SerializeObject(u)
        });

    }

    public void Edit(EditUsuarioDto dto)
    {
        var u = _repo.GetById(dto.IdUsuario) ?? throw new Exception("Usuario no encontrado");
        var usuarioExistente = u;
        u.Edit(dto.Nombres, dto.PrimerApellido, dto.SegundoApellido, dto.Identificacion, dto.CorreoElectronico, dto.Estado);
        _repo.Update(u);

        //registrar evento en bitacora
        _bitacoraService.RegisterEvento(new BitacoraEventoDto
        {
            TablaDeEvento = "USUARIOS",
            TipoDeEvento = "Editar",
            DescripcionDeEvento = "Edicion de usuario",
            StackTrace = "",
            DatosAnteriores = JsonConvert.SerializeObject(usuarioExistente),
            DatosPosteriores = JsonConvert.SerializeObject(u)
        });
    }
}
