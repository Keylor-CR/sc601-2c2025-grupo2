using SinpeEmpresarial.Application.Dtos;
using SinpeEmpresarial.Application.Dtos.ConfiguracionComercio;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ConfiguracionComercioController : Controller
    {

        private readonly IConfiguracionComercioService _configuracioncomercioService;

        private void LlenarViewBags(int? tipoDeCofiguracionSeleccionado = null)
        {
            ViewBag.TipoConfiguracion = new SelectList(
                new[]
                {
                    new { Value = 1, Text = "Plataforma" },
                    new { Value = 2, Text = "Externa" },
                    new { Value = 3, Text = "Ambas" }
                },
                "Value", "Text"
            );
        }

        public ConfiguracionComercioController(IConfiguracionComercioService configuracioncomercioService)
        {
            _configuracioncomercioService = configuracioncomercioService;
        }

        // GET: Configuracion Comercio
        public ActionResult Index()
        {
            var configuraciones = _configuracioncomercioService.GetAllConfiguraciones();
            return View(configuraciones);
        }

        // GET: Configuracion Comercio/Details/5
        public ActionResult Details(int id)
        {
            var configuracion = _configuracioncomercioService.GetConfiguracionByComercio(id);
            if (configuracion == null)
                return HttpNotFound();
            return View(configuracion);
        }

        // GET: Configuracion Comercio/Create
        public ActionResult Create(int idComercio, string nombre)
        {
            var model = new ConfiguracionComercioCreateDto { IdComercio = idComercio };
            ViewBag.NombreComercio = nombre;
            LlenarViewBags();
            return View(model);
        }

        // POST: Configuracion Comercio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConfiguracionComercioCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                LlenarViewBags();
                return View(dto);
            }

            try
            {
                _configuracioncomercioService.RegisterConfiguracionComercio(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LlenarViewBags();
                return View(dto);
            }
        }
        // GET: Configuracion Comercio/Edit
        public ActionResult Edit(int idComercio, string nombre)
        {
            var configuracion = _configuracioncomercioService.GetConfiguracionByComercio(idComercio);
            if (configuracion == null)
                return HttpNotFound();

            var editDto = new ConfiguracionComercioEditDto
            { 
                IdConfiguracion = configuracion.IdConfiguracion,
                IdComercio = idComercio,
                TipoConfiguracion = configuracion.TipoConfiguracion,
                Comision = configuracion.Comision,
                Estado = configuracion.Estado
            };
            ViewBag.NombreComercio = nombre;
            LlenarViewBags(editDto.TipoConfiguracion);
            return View(editDto);
        }
    }
}