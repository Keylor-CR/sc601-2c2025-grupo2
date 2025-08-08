using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SinpeEmpresarial.Web.Models;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Infrastructure.Data;

namespace SinpeEmpresarial.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly SinpeDbContext _domainContext;

        public AccountController()
        {
            _domainContext = new SinpeDbContext();
        }

        public ApplicationUserManager UserManager =>
            _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ApplicationSignInManager SignInManager =>
            _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        private IAuthenticationManager AuthenticationManager =>
            HttpContext.GetOwinContext().Authentication;

        // GET: /Account/Login
        public ActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await SignInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Credenciales inválidas.");
                    return View(model);
            }
        }

        // GET: /Account/Register
        public ActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Validar existencia del cajero en la tabla Usuarios
            Usuario usuarioExistente = null;

            if (model.Rol == "Cajero")
            {
                usuarioExistente = _domainContext.Usuarios
                    .FirstOrDefault(u =>
                        u.CorreoElectronico == model.Email &&
                        u.Estado == true);

                if (usuarioExistente == null)
                {
                    ModelState.AddModelError("", "El cajero no existe en la tabla Usuarios.");
                    return View(model);
                }
            }

            // Crear el usuario con Identity
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, model.Rol);

                if (usuarioExistente != null)
                {
                    // Actualizar el campo IdNetUser en la tabla Usuarios
                    usuarioExistente.IdNetUser = Guid.Parse(user.Id); // Asegúrate que user.Id es un GUID válido
                    _domainContext.SaveChanges();
                }

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", "Home");
            }

            // Mostrar errores si algo falló
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View(model);
        }

        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
