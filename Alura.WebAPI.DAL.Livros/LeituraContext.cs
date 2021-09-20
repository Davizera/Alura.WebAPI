using Alura.ListaLeitura.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Alura.ListaLeitura.Persistencia
{
    public class LeituraContext : DbContext
    {
        public LeituraContext(DbContextOptions<LeituraContext> options)
            : base(options)
        {
            //irá criar o banco e a estrutura de tabelas necessárias
            Database.EnsureCreated();
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LivroConfiguration());
        }
    }
}