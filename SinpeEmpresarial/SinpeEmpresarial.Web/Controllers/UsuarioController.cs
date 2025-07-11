using SinpeEmpresarial.Application.DTOs.Usuario;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;

public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public ActionResult Index()
    {
        var usuarios = _usuarioService.GetAll();
        return View(usuarios);
    }

    public ActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateUsuarioDto dto)
    {
        try
        {
            _usuarioService.Register(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    public ActionResult Edit(int id)
    {
        var u = _usuarioService.GetById(id);
        if (u == null) return HttpNotFound();

        var dto = new EditUsuarioDto
        {
            IdUsuario = u.IdUsuario,
            Nombres = u.Nombres,
            PrimerApellido = u.PrimerApellido,
            SegundoApellido = u.SegundoApellido,
            Identificacion = u.Identificacion,
            CorreoElectronico = u.CorreoElectronico,
            Estado = u.Estado
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(EditUsuarioDto dto)
    {
        try
        {
            _usuarioService.Edit(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }
}
