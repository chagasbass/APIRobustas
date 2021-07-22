using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using ApiRobustas.Dominio.Contextos.Produtos.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace ApiRobustas.Infraestrutura.Data.ContextosDeDados
{
    public class ContextoDeDadosEfCore : DbContext
    {
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public ContextoDeDadosEfCore(DbContextOptions<ContextoDeDadosEfCore> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<Notification>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
