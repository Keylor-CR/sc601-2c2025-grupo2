using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ReportesMensualesController : Controller
    {
        private readonly IReportesMensualesService _reportesMensualesService;
        private readonly IBitacoraService _bitacoraService;

        public ReportesMensualesController(IReportesMensualesService reportesMensualesService, IBitacoraService bitacoraService)
        {
            _reportesMensualesService = reportesMensualesService;
            _bitacoraService = bitacoraService;
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
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "REPORTES_MENSUALES",
                    TipoDeEvento = "GENERACIÓN",
                    DescripcionDeEvento = "Generación de reportes mensuales iniciada por usuario",
                    FechaDeEvento = DateTime.Now,
                    DatosAnteriores = "N/A",
                    DatosPosteriores = "Proceso de generación de reportes completado"
                });
                TempData["Success"] = "Reportes generados exitosamente";
            }
            catch (System.Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "REPORTES_MENSUALES",
                    TipoDeEvento = "ERROR",
                    DescripcionDeEvento = "Error al generar reportes mensuales",
                    FechaDeEvento = DateTime.Now,
                    StackTrace = ex.StackTrace,
                    DatosAnteriores = "N/A",
                    DatosPosteriores = ex.Message
                });
                TempData["Error"] = "Error al generar reportes: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
