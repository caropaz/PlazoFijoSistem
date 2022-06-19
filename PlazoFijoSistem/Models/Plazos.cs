using System.ComponentModel.DataAnnotations;

namespace PlazoFijoSistem.Models
{
    public class Plazos
    {
        [Key]
        public int Id { get; set; }
        public int Monto { get; set; }
        public int Dias { get; set; }
        public Bancos Bancos { get; set; }
    }
}
