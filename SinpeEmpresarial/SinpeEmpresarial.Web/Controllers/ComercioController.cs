using SinpeEmpresarial.Application.DTOs;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class ComercioController : Controller
    {
        private readonly IComercioService _comercioService;

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
            return View();
        }

        // POST: Comercio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComercioCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                _comercioService.Register(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
                Id = id,
                Nombre = comercio.Nombre,
                TipoDeComercio = comercio.TipoDeComercio,
                Telefono = comercio.Telefono,
                CorreoElectronico = comercio.CorreoElectronico,
                Direccion = comercio.Direccion,
                Estado = comercio.Estado
            };
            return View(editDto);
        }

        // POST: Comercio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComercioEditDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                _comercioService.Edit(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
    }
}