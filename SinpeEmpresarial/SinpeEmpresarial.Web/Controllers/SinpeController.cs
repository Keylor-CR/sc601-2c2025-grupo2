using SinpeEmpresarial.Application.DTOs.Sinpe;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    public class SinpeController : Controller
    {
        private readonly ISinpeService _sinpeService;

        public SinpeController(ISinpeService sinpeService)
        {
            _sinpeService = sinpeService ?? throw new ArgumentNullException(nameof(sinpeService));
        }

        // GET: Sinpe/Create
        public ActionResult Create()
        {
            var model = new SinpeCreateDto();
            return View(model);
        }

        // POST: Sinpe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SinpeCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = _sinpeService.RegisterSinpe(model);
                
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrio un error al procesar el registro SINPE: " + ex.Message);
                return View(model);
            }
        }

        // GET: Sinpe/Success
        public ActionResult Success()
        {
            if (TempData["SuccessMessage"] == null)
            {
                return RedirectToAction("Create");
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }
    }
}
