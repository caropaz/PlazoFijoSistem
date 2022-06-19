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
        public IActionResult Index(String email, String contraseña) // aca voy a filtrar si es adm o user
        {
            bool usuarioExiste = false;
            usuarioExiste=_context.Usuarios.Any(o => o.Email.Equals(email) );
            if (usuarioExiste) 
            {
                return RedirectToAction("Index", "Plazos");
            }
            
            return RedirectToAction("Create","Usuarios");
        }
    }
}
