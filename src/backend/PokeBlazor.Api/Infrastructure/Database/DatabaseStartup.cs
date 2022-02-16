using PokeBlazorApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PokeBlazorApi.Infrastructure.Database
{
    public static class DatabaseStartup
    {
        public static IServiceCollection ConfigureDatabaseServer(this IServiceCollection services,
            IConfiguration Configuration)
        {
            return services.AddDbContext<PokemonContext>(options => options.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=docker"));
        }
    }
}