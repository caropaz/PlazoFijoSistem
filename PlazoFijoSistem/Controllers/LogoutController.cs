using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlazoFijoSistem.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
            
        }
    }
}
