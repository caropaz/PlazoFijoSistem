using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PlazoFijoSistem.Controllers
{
    public class ControladorBase : Controller
    {
        public bool EsAdmin 
        {
            get 
            {
                string rol = User.FindFirstValue(ClaimTypes.Role);
                return rol.Equals("ADMIN");
            }
         }

        
            public int IdUsuario
        {
            get
            {
                string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.Parse(idUsuario);
            }
        }
    }
}
