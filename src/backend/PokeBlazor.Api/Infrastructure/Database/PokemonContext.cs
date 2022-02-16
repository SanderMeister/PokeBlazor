using PokeBlazorApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace PokeBlazorApi.Infrastructure.Database
{
  public class PokemonContext : DbContext
  {
    public DbSet<PokemonEntity> Pokemons { get; set; }
    public PokemonContext(DbContextOptions<PokemonContext> options) : base(options) {
    }
  }
}
