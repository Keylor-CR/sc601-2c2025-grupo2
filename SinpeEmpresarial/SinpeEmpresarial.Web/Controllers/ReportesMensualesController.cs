using SinpeEmpresarial.Application.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ReportesMensualesController : Controller
    {
        private readonly IReportesMensualesService _reportesMensualesService;

        public ReportesMensualesController(IReportesMensualesService reportesMensualesService)
        {
            _reportesMensualesService = reportesMensualesService;
        }

        public ActionResult Index()
        {
            try
            {
                var reportes = _reportesMensualesService.GetAllReportes();
                return View(reportes);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al cargar los reportes: " + ex.Message;
                return View(new List<SinpeEmpresarial.Application.DTOs.ReportesMensuales.ListReportesMensualesDto>());
            }
        }

        public ActionResult Generar()
        {
            try
            {
                _reportesMensualesService.GenerarReportesMensuales();
                TempData["Success"] = "Reportes generados exitosamente";
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = "Error al generar reportes: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
