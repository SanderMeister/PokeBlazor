using System;
using System.Linq;
using System.Threading.Tasks;
using PokeBlazor.Models;

namespace PokeBlazor.Services
{
    public interface IGetPokemonService
    {
        Task<Pokemon> GetRandomPokemon();
        Task<Pokemon> GetPokemonById(int id);
    }
}
