using Microsoft.AspNetCore.Mvc;
using PokedexBackend.Controllers.RequestModels;
using PokedexBackend.Controllers.ResponseModels;
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
    [ProducesResponseType(typeof(PokemonResponse[]), 200)]
    public async Task<IActionResult> Get()
    {
        var pokemons = await _pokemonsRepo.GetAll("Attacks");
        return Ok(pokemons.Select(PokemonResponse.fromDbo));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PokemonResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(int id)
    {
        Pokemon? found = await _pokemonsRepo.GetById(id, "Attacks");
        return found == null ? NotFound() : Ok(PokemonResponse.fromDbo(found));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PokemonResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] PokemonRequest pokemon)
    {
        Pokemon? newPokemon = await _pokemonsRepo.Insert(pokemon.toDbo());
        return newPokemon == null ? BadRequest() : Ok(PokemonResponse.fromDbo(newPokemon));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PokemonResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(int id, [FromBody] PokemonRequest pokemon)
    {
        Pokemon? newPokemon = await _pokemonsRepo.Update(pokemon.toDbo(id));
        return newPokemon == null ? BadRequest() : Ok(PokemonResponse.fromDbo(newPokemon));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _pokemonsRepo.Delete(id);
        return success ? NoContent() : Conflict();
    }

    [HttpPut("{id}/attacks/{attack_id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddAttack(int id, int attack_id)
    {
        bool success = await _pokemonsRepo.AddAttack(id, attack_id);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}/attacks/{attack_id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveAttack(int id, int attack_id)
    {
        bool success = await _pokemonsRepo.RemoveAttack(id, attack_id);
        return success ? NoContent() : NotFound();
    }
}
