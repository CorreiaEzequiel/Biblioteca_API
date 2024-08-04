using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Biblioteca.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Livro> Livro { get; set; }

    }
}
