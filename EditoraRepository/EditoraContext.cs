using Editora.Domain;
using Editora.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Editora.Repository
{
    public class EditoraContext : DbContext
    {
        public DbSet<Autor> Autores {get; set;}
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public EditoraContext(DbContextOptions<EditoraContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
