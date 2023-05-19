using Microsoft.AspNetCore.Mvc;
using PokedexBackend.Controllers.RequestModels;
using PokedexBackend.Controllers.ResponseModels;
using PokedexBackend.DataAccess.Repositories;
using PokedexBackend.Dbo;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokedexBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsersRepository _usersRepo;

    public UserController(IUsersRepository usersRepo)
    {
        _usersRepo = usersRepo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserResponse[]), 200)]
    public async Task<IActionResult> Get()
    {
        var users = await _usersRepo.GetAll("Pokemons");
        return Ok(users.Select(UserResponse.fromDbo));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(int id)
    {
        User? found = await _usersRepo.GetById(id, "Pokemons");
        return found == null ? NotFound() : Ok(UserResponse.fromDbo(found));
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] UserRequest user)
    {
        User? newUser = await _usersRepo.Insert(user.toDbo());
        return newUser == null ? BadRequest() : Ok(UserResponse.fromDbo(newUser));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(int id, [FromBody] UserRequest user)
    {
        User? newUser = await _usersRepo.Update(user.toDbo(id));
        return newUser == null ? BadRequest() : Ok(UserResponse.fromDbo(newUser));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _usersRepo.Delete(id);
        return success ? NoContent() : Conflict();
    }

    [HttpPut("{id}/favorites/{pokemon_id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddFavorite(int id, int pokemon_id)
    {
        bool success = await _usersRepo.AddFavorite(id, pokemon_id);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}/favorites/{pokemon_id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveFavorite(int id, int pokemon_id)
    {
        bool success = await _usersRepo.RemoveFavorite(id, pokemon_id);
        return success ? NoContent() : NotFound();
    }
}
