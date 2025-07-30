using SinpeEmpresarial.Application.Dtos.ConfiguracionComercio;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ConfiguracionComercioController : Controller
    {

        private readonly IConfiguracionComercioService _configuracioncomercioService;
        private readonly IComercioService _comercioService;

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

        public ConfiguracionComercioController(IConfiguracionComercioService configuracioncomercioService, IComercioService comercioService)
        {
            _configuracioncomercioService = configuracioncomercioService;
            _comercioService = comercioService;
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
            var configuracion = _configuracioncomercioService.GetConfiguracionById(id);
            if (configuracion == null)
                return HttpNotFound();
            return View(configuracion);
        }

        // GET: Configuracion Comercio/Create
        public ActionResult Create(int idComercio)
        {
            var model = new ConfiguracionComercioCreateDto { IdComercio = idComercio };
            var comercio = _comercioService.GetComercioById(idComercio);
            ViewBag.NombreComercio = comercio.Nombre;
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

            _configuracioncomercioService.RegisterConfiguracionComercio(dto);
            return RedirectToAction("Index");
        }
        // GET: ConfiguracionComercio/Edit
        public ActionResult Edit(int idComercio)
        {
            var configuracion = _configuracioncomercioService.GetConfiguracionByComercio(idComercio);
            if (configuracion == null)
                return HttpNotFound();

            var comercio = _comercioService.GetComercioById(idComercio);

            var editDto = new ConfiguracionComercioEditDto
            { 
                IdConfiguracion = configuracion.IdConfiguracion,
                IdComercio = idComercio,
                TipoConfiguracion = configuracion.TipoConfiguracion,
                Comision = configuracion.Comision,
                Estado = configuracion.Estado
            };
            ViewBag.NombreComercio = comercio.Nombre;
            LlenarViewBags(editDto.TipoConfiguracion);
            return View(editDto);
        }

        // POST: ConfiguracionComercio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConfiguracionComercioEditDto editDto)
        {
            if (!ModelState.IsValid)
            {
                LlenarViewBags(editDto.TipoConfiguracion);
                return View(editDto);
            }
            _configuracioncomercioService.EditConfiguracionComercio(editDto);
            return RedirectToAction("Index");
        }
    }
}