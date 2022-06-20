using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlazoFijoSistem.Models
{
    public class Plazos
    {
        [Key]
        public int Id { get; set; }
        public int Monto { get; set; }
        public int Dias { get; set; }
        public int BancoId { get; set; }

        [ForeignKey("BancoId")]
        public virtual Bancos? Banco { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuarios? Usuario { get; set; }
    }
}
