using System;
using System.Net.Http;
using System.Text.Json;
using PokeBlazorApi.Models;
using PokeBlazorApi.Entities;
using Microsoft.AspNetCore.Mvc;
using PokeBlazorApi.Infrastructure.Database;
using System.Linq;

namespace PokeBlazorApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PokemonController : ControllerBase
  {
    private readonly PokemonContext _context;

    public PokemonController(PokemonContext context)
    {
      _context = context;
    }

    private static JsonSerializerOptions jsonSettings = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    [HttpGet("random")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Pokemon GetRandomPokemon()
    {
      Random r = new Random();
      using (var httpClient = new HttpClient())
      {
        var pokemonNumber = r.Next(0, 898);
        var response = httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonNumber}").Result;
        var t = response.Content.ReadAsStringAsync().Result;
        var pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content.ReadAsStringAsync().Result, jsonSettings);

        var dbPokemon = _context.Pokemons.SingleOrDefault(pkmn => pkmn.Id == pokemon.Id);
        if(dbPokemon == null){
            var pkmnEntity = new PokemonEntity();
            pkmnEntity.Id = pokemon.Id;
            pkmnEntity.Name = pokemon.Name;
            pkmnEntity.Count = 0;
            _context.Pokemons.Add(pkmnEntity);
        } else {
          dbPokemon.Count += 1;
        }

        _context.SaveChanges();

        return pokemon;
      }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Pokemon GetPokemonById(int id)
    {
      using (var httpClient = new HttpClient())
      {
        var response = httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}").Result;
        var pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content.ReadAsStringAsync().Result, jsonSettings);
        return pokemon;
      }
    }
  }
}