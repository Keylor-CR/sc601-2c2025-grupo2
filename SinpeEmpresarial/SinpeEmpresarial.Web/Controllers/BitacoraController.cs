using SinpeEmpresarial.Application.DTOs;
using SinpeEmpresarial.Application.Interfaces;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly IBitacoraService _bitacoraService;
        public BitacoraController(IBitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }

        // GET: Bitacora
        public ActionResult Index()
        {
            var eventos = _bitacoraService.GetAllEventos();
            return View(eventos);
        }

    }
}