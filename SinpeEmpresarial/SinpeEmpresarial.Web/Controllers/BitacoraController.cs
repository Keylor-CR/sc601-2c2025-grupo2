using SinpeEmpresarial.Application.Dtos;
using SinpeEmpresarial.Application.Interfaces;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
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