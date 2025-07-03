using System.Web.Mvc;
using SinpeEmpresarial.Application.Interfaces;

public class HomeController : Controller
{
    private readonly IComercioService _comercioService;
    private readonly ISinpeService _sinpeService;
    private readonly IBitacoraService _bitacoraService;

    public HomeController(
        IComercioService comercioService,
        ISinpeService sinpeService,
        IBitacoraService bitacoraService)
    {
        _comercioService = comercioService;
        _sinpeService = sinpeService;
        _bitacoraService = bitacoraService;
    }

    public ActionResult Index()
    {
        var comercios = _comercioService.GetAllComercios();
        var sinpes = _sinpeService.GetLast(5);
        var bitacoras = _bitacoraService.GetLast(5);

        ViewBag.Comercios = comercios;
        ViewBag.Sinpes = sinpes;
        ViewBag.Bitacoras = bitacoras;

        return View();
    }
}
