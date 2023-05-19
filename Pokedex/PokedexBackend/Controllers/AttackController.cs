using Microsoft.AspNetCore.Mvc;
using PokedexBackend.Controllers.RequestModels;
using PokedexBackend.Controllers.ResponseModels;
using PokedexBackend.DataAccess.Repositories;
using PokedexBackend.Dbo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokedexBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttackController : ControllerBase
{
    private readonly IAttacksRepository _attacksRepo;

    public AttackController(IAttacksRepository attacksRepo)
    {
        _attacksRepo = attacksRepo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AttackResponse[]), 200)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _attacksRepo.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttackResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(int id)
    {
        Attack? found = await _attacksRepo.GetById(id);
        return found == null ? NotFound() : Ok(found);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AttackResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post([FromBody] AttackRequest attack)
    {
        Attack? newAttack = await _attacksRepo.Insert(attack.toDbo());
        return newAttack == null ? BadRequest() : Ok(newAttack);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttackResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(int id, [FromBody] AttackRequest attack)
    {
        Attack? newAttack = await _attacksRepo.Update(attack.toDbo(id));
        return newAttack == null ? BadRequest() : Ok(newAttack);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _attacksRepo.Delete(id);
        return success ? NoContent() : Conflict();
    }
}
