using SinpeEmpresarial.Application.Dtos.Caja;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using System;
using System.Linq;
using System.Web.Mvc;

[Authorize(Roles = "Cajero, Administrador")]
public class CajaController : Controller
{
    private readonly ICajaService _cajaService;
    private readonly ISinpeService _sinpeService;
    private readonly IComercioService _comercioService;

    public CajaController(ICajaService cajaService, ISinpeService sinpeService, IComercioService comercioService)
    {
        _cajaService = cajaService;
        _sinpeService = sinpeService;
        _comercioService = comercioService;
    }

    public ActionResult Index()
    {
        var allCajas = _cajaService.GetAll();
        return View(allCajas);
    }

    public ActionResult PorComercio(int comercioId)
    {
        var cajas = _cajaService.GetCajasByComercio(comercioId);
        return View("Index", cajas);
    }

    public ActionResult Create()
    {
        var comercios = _comercioService.GetAllComercios();
        ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
        return View();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public ActionResult Create(CreateCajaDto dto)
    {
        _cajaService.AddCaja(dto);
        return RedirectToAction("PorComercio", new { comercioId = dto.IdComercio });
    }

    public ActionResult Edit(int id)
    {
        var caja = _cajaService.GetById(id);
        return View(new EditCajaDto
        {
            IdCaja = caja.IdCaja,
            Nombre = caja.Nombre,
            Descripcion = caja.Descripcion,
            TelefonoSINPE = caja.TelefonoSINPE,
            Estado = caja.Estado
        });
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public ActionResult Edit(EditCajaDto dto)
    {
        _cajaService.EditCaja(dto);
        return RedirectToAction("Index");
    }

    public ActionResult VerSinpe(string telefonoSINPE)
    {
        var sinpes = _sinpeService.GetByCajaTelefono(telefonoSINPE);

        // Debug step: see how many were returned
        ViewBag.DebugCount = sinpes.Count;
        ViewBag.Tel = telefonoSINPE;

        return View(sinpes);
    }
    [HttpPost]
    public ActionResult Sincronizar(int id, string telefonoDestino)
    {
        _sinpeService.Sincronizar(id);
        return RedirectToAction("VerSinpe", new { telefonoSINPE = telefonoDestino });
    }
}
