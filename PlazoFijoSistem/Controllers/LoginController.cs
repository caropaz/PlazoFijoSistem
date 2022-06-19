using Microsoft.AspNetCore.Mvc;
using PlazoFijoSistem.Datos;

namespace PlazoFijoSistem.Controllers
{
    public class LoginController : Controller
    {
        private readonly BaseDeDatos _context;

        public LoginController(BaseDeDatos context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(String email, String contraseña)
        {
            
            return RedirectToAction("Create","Usuarios");
        }
    }
}
