using System.ComponentModel.DataAnnotations;

namespace PlazoFijoSistem.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public String Apellido { get; set; } 
        public String Nombre { get; set; } 
        public String Email { get; set; } 
        public String Password { get; set; }

    }
}
