using System.ComponentModel.DataAnnotations;

namespace PlazoFijoSistem.Models
{
    public class Bancos
    {
        [Key]
        public int id { get; set; } 

        public String RazonSocial { get; set; }

        public decimal Porcentaje { get; set; }

        public virtual IEnumerable<Plazos>? plazos { get; set; }
    }
}
