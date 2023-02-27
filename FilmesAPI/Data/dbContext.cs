using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data {
    public class dbContext : DbContext {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        
        public DbSet<Cinema> Cinemas { get; set; }
        
        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Sessao> Sessoes { get; set; }
    }
}
