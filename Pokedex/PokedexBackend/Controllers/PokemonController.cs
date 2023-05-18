using Microsoft.AspNetCore.Mvc;
using PokedexBackend.DataAccess.Repositories;
using PokedexBackend.Dbo;

namespace PokedexBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonsRepository _pokemonsRepo;

    public PokemonController(IPokemonsRepository pokemonsRepo)
    {
        _pokemonsRepo = pokemonsRepo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pokemon[]), 200)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _pokemonsRepo.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Pokemon), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(int id)
    {
        Pokemon? found = await _pokemonsRepo.GetById(id);
        return found == null ? NotFound() : Ok(found);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Pokemon), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] Pokemon pokemon)
    {
        return Ok(await _pokemonsRepo.Insert(pokemon));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Pokemon), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(int id, [FromBody] Pokemon pokemon)
    {
        pokemon.Id = id;
        Pokemon? newPokemon = await _pokemonsRepo.Update(pokemon);
        return newPokemon == null ? BadRequest() : Ok(newPokemon);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _pokemonsRepo.Delete(id);
        return success ? NoContent() : Conflict();
    }
}
