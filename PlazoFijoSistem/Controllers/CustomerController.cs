

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlazoFijoSistem.Datos;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PlazoFijoSistem.Models;

namespace PlazoFijoSistem.Controllers
{

    [Authorize(Roles = "ADMIN")]
    public class Customer: Controller
    {
        private readonly BaseDeDatos _context;

        public Customer(BaseDeDatos context)
        {
            _context = context;
        }


        public ActionResult Index(string email,string banco)
        {
            if (email != null)
            {
                var usuarios = _context
                      .Plazos
                      .Where(o => o.Usuario.Email.ToUpper().Equals(email))
                      .Include(p => p.Banco)
                      .Include(p => p.Usuario);
                if (usuarios != null)
                {
                    return View(usuarios);
                }
            }
            else if (banco!=null)
            {
                var bancos = _context
                      .Plazos
                      .Where(o => o.Banco.RazonSocial.ToUpper().Equals(banco))
                      .Include(p => p.Banco)
                      .Include(p => p.Usuario);
                if (bancos != null)
                {
                    return View(bancos);
                }
            }
            return View();
        }

        /*public ActionResult Buscar(string email)
        {
            var usuarios = _context
                      .Plazos
                      .Include(p => p.Banco)
                      .Include(p => p.Usuario);

            var busqueda = from s in _context.Plazos select s;

            if (!String.IsNullOrEmpty(email))
            {
                busqueda = busqueda.Where(s => s.Usuario.Email.Contains(email)
                                       || s.Usuario.Email.Contains(email));
            }
            return View(busqueda.ToList());
        }*/













    }
}
