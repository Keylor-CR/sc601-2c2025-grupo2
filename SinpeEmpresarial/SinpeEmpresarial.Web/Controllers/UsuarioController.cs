using SinpeEmpresarial.Application.DTOs.Usuario;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using System;
using System.Web.Mvc;

public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IComercioService _comercioService;

    public UsuarioController(IUsuarioService usuarioService, IComercioService comercioService)
    {
        _usuarioService = usuarioService;
        _comercioService = comercioService;
    }

    public ActionResult Index()
    {
        var usuarios = _usuarioService.GetAll();
        return View(usuarios);
    }

    public ActionResult Create()
    {
        var comercios = _comercioService.GetAllComercios();
        ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
        return View();
    }

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
            var comercios = _comercioService.GetAllComercios();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
            return View(dto);
        }
    }

    public ActionResult Edit(int id)
    {
        var u = _usuarioService.GetById(id);
        if (u == null) return HttpNotFound();

        var comercios = _comercioService.GetAllComercios();
        ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", u.IdComercio);

        var dto = new EditUsuarioDto
        {
            IdUsuario = u.IdUsuario,
            Nombres = u.Nombres,
            PrimerApellido = u.PrimerApellido,
            SegundoApellido = u.SegundoApellido,
            Identificacion = u.Identificacion,
            CorreoElectronico = u.CorreoElectronico,
            Estado = u.Estado,
            IdComercio = u.IdComercio
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
            var comercios = _comercioService.GetAllComercios();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", dto.IdComercio);
            return View(dto);
        }
    }
}
