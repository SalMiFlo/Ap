using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;

namespace AplicacionEFCore
{
    public class ApplicationDbSetContext : IdentityDbContext //Se hereda de la clase Identity para el control de usuarios
    {
        public ApplicationDbSetContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Paso> Pasos { get; set; } 
        public DbSet<ArchivoAdjunto> ArchivosAdjuntos { get; set; }
    }
}