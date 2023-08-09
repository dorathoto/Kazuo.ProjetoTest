using Kazuo.ProjetoTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kazuo.ProjetoTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Funcao, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Id)
                .HasColumnName("UserId");

            modelBuilder.Entity<Funcao>()
                .Property(e => e.Id)
                .HasColumnName("RoleId");
        }
    }
}
