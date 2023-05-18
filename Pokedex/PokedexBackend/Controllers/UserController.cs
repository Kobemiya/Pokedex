using Microsoft.AspNetCore.Mvc;
using PokedexBackend.DataAccess.Repositories;
using PokedexBackend.Dbo;

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
    [ProducesResponseType(typeof(User[]), 200)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _usersRepo.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(int id)
    {
        User? found = await _usersRepo.GetById(id);
        return found == null ? NotFound() : Ok(found);
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        User? newUser = await _usersRepo.Insert(user);
        return newUser == null ? BadRequest() : Ok(newUser);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(int id, [FromBody] User user)
    {
        user.Id = id;
        User? newUser = await _usersRepo.Update(user);
        return newUser == null ? BadRequest() : Ok(newUser);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _usersRepo.Delete(id);
        return success ? NoContent() : Conflict();
    }
}
