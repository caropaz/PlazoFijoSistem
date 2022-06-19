using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PlazoFijoSistem.Datos;
using System.Security.Claims;

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
        {      //ClaimsIdentity: crea una cookie, donde el claimtype tiene diferentes opciones, preestablecidas.
              
            // hay que buscar el usuario 
            var usuario = _context
                .Usuarios
                .Where(o => o.Email.Equals(email))
                .FirstOrDefault();
            if (email == "caropaz@gmail.com") 
            {
                
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // aca vamos agregando a la cookie datos
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
                identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                // en el siguiente paso, con todo lo seteado en el addclaim, se crea el principal Claim,

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // Y aca se hace el login del usuario al sistema, con la cooki
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                //y luego se hace el redirectiontoaction

                return RedirectToAction("Index", "Usuarios");
            }

            // se hace exactamente lo mismo que en el paso anterior, con todo lo que hicimos antes del claim

            bool usuarioExiste = usuario != null; //significa que el usuario existe, es decir q lo encontro
            if (usuarioExiste) 
            {
               
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // aca vamos agregando a la cookie datos
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
                identity.AddClaim(new Claim(ClaimTypes.Role, "USER"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                // en el siguiente paso, con todo lo seteado en el addclaim, se crea el principal Claim,

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // Y aca se hace el login del usuario al sistema, con la cooki
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                //y luego se hace el redirectiontoaction
                return RedirectToAction("Index", "Plazos");
            }
            
            return RedirectToAction("Create","Usuarios");
        }
    }
}
