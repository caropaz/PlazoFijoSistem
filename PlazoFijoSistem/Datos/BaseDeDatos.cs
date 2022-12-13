using Microsoft.EntityFrameworkCore;
using PlazoFijoSistem.Models;
namespace PlazoFijoSistem.Datos
{
    public class BaseDeDatos : DbContext
    {
        public BaseDeDatos(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Plazos> Plazos { get; set; }

        public DbSet<Bancos> Bancos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=DESKTOP-1CM86AD\CAROLINAPAZ;Database=PlazoFijoSistem;Trusted_Connection=True;MultipleActiveResultSets=true;");

        }


    }
}
