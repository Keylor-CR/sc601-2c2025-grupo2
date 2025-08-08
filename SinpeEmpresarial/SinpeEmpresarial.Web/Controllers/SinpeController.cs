using SinpeEmpresarial.Application.Dtos.Sinpe;
using SinpeEmpresarial.Application.Interfaces;
using System;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
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
            var result = _sinpeService.RegisterSinpe(model);
            
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Success");
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
