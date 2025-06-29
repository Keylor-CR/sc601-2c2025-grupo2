using SinpeEmpresarial.Application.DTOs.Caja;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;
using System.Linq;


public class CajaController : Controller
{
    private readonly ICajaService _cajaService;
    private readonly ISinpeService _sinpeService;

    public CajaController(ICajaService cajaService, ISinpeService sinpeService)
    {
        _cajaService = cajaService;
        _sinpeService = sinpeService;
    }

    // ✅ All Cajas
    public ActionResult Index()
    {
        var allCajas = _cajaService.GetAll();
        return View(allCajas);
    }

    // ✅ Cajas by Comercio
    public ActionResult PorComercio(int comercioId)
    {
        var cajas = _cajaService.GetCajasByComercio(comercioId);
        return View("Index", cajas);
    }

    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(CreateCajaDto dto)
    {
        try
        {
            _cajaService.AddCaja(dto);
            return RedirectToAction("PorComercio", new { comercioId = dto.IdComercio });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    public ActionResult Edit(int id)
    {
        var caja = _cajaService.GetCajasByComercio(0).FirstOrDefault(c => c.IdCaja == id);
        return View(new EditCajaDto
        {
            IdCaja = caja.IdCaja,
            Nombre = caja.Nombre,
            Descripcion = caja.Descripcion,
            TelefonoSINPE = caja.TelefonoSINPE,
            Estado = caja.Estado
        });
    }

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

}
