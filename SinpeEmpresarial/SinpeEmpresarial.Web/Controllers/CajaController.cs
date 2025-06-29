using SinpeEmpresarial.Application.DTOs.Caja;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using System;
using System.Linq;
using System.Web.Mvc;


public class CajaController : Controller
{
    private readonly ICajaService _cajaService;
    private readonly ISinpeService _sinpeService;

    public CajaController(ICajaService cajaService, ISinpeService sinpeService)
    {
        _cajaService = cajaService;
        _sinpeService = sinpeService;
    }


    public ActionResult Index(int? comercioId)
    {
        if (comercioId == null)
        {
            // Handle gracefully (you could redirect to select comercio)
            return RedirectToAction("Index", "Comercio");
        }

        var cajas = _cajaService.GetCajasByComercio(comercioId.Value);
        return View(cajas);
    }


    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(CreateCajaDto dto)
    {
        try
        {
            _cajaService.AddCaja(dto);
            return RedirectToAction("Index", new { comercioId = dto.IdComercio });
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
        var movimientos = _sinpeService.GetByCajaTelefono(telefonoSINPE);
        return View(movimientos);
    }

}
