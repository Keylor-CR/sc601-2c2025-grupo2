using SinpeEmpresarial.Application.DTOs;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ComercioController : Controller
    {
        private readonly IComercioService _comercioService;

        private void LlenarViewBags(int? tipoDeComercioSeleccionado = null)
        {
            ViewBag.TipoIdentificacion = new SelectList(
                new[]
                {
                    new { Value = 1, Text = "Física" },
                    new { Value = 2, Text = "Jurídica" }
                },
                "Value", "Text"
            );

            ViewBag.TipoDeComercio = new SelectList(
                new[]
                {
                    new { Value = 1, Text = "Restaurantes" },
                    new { Value = 2, Text = "Supermercados" },
                    new { Value = 3, Text = "Ferreterías" },
                    new { Value = 4, Text = "Otros" }
                },
                "Value", "Text", tipoDeComercioSeleccionado
            );
        }

        public ComercioController(IComercioService comercioService)
        {
            _comercioService = comercioService;
        }

        // GET: Comercio
        public ActionResult Index()
        {
            var comercios = _comercioService.GetAll();
            return View(comercios);
        }

        // GET: Comercio/Details/5
        public ActionResult Details(int id)
        {
            var comercio = _comercioService.GetById(id);
            if (comercio == null)
                return HttpNotFound();
            return View(comercio);
        }

        // GET: Comercio/Create
        public ActionResult Create()
        {
            LlenarViewBags();
            return View();
        }

        // POST: Comercio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComercioCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                LlenarViewBags();
                return View(dto);
            }

            try
            {
                _comercioService.Register(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LlenarViewBags();
                return View(dto);
            }
        }

        // GET: Comercio/Edit/5
        public ActionResult Edit(int id)
        {
            var comercio = _comercioService.GetById(id);
            if (comercio == null)
                return HttpNotFound();

            var editDto = new ComercioEditDTO
            {
                IdComercio = id,
                Nombre = comercio.Nombre,
                TipoDeComercio = comercio.TipoDeComercio,
                Telefono = comercio.Telefono,
                CorreoElectronico = comercio.CorreoElectronico,
                Direccion = comercio.Direccion,
                Estado = comercio.Estado
            };
            LlenarViewBags(editDto.TipoDeComercio);
            return View(editDto);
        }

        // POST: Comercio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComercioEditDTO editDto)
        {
            if (!ModelState.IsValid)
            {
                LlenarViewBags(editDto.TipoDeComercio);
                return View(editDto);
            }
                

            try
            {
                _comercioService.Edit(editDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LlenarViewBags(editDto.TipoDeComercio);
                return View(editDto);
            }
        }
    }
}