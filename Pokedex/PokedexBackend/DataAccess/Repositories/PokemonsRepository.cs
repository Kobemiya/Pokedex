using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokedexBackend.DataAccess.EfModels;
using System.Data;

namespace PokedexBackend.DataAccess.Repositories;

public class PokemonsRepository : CRUDRepository<Pokemon, Dbo.Pokemon>, IPokemonsRepository
{
    protected readonly DbSet<Attack> _attacksSet;

    public PokemonsRepository(PokedexDotNetContext context, ILogger<CRUDRepository<Pokemon, Dbo.Pokemon>> logger, IMapper mapper) :
        base(context, logger, mapper)
    {
        _attacksSet = _context.Set<Attack>();
    }

    public async Task<bool> AddAttack(long pokemon_id, long attack_id)
    {
        try
        {
            Pokemon pokemonEntity = _set.Include("Attacks").First(p => p.Id == pokemon_id) ?? throw new DataException($"Entity of type Pokemon not found with id {pokemon_id}");
            Attack attackEntity = _attacksSet.Find(attack_id) ?? throw new DataException($"Entity of type Attack not found with id {pokemon_id}");

            if (!pokemonEntity.Attacks.Any(a => a.Id == attack_id))
                pokemonEntity.Attacks.Add(attackEntity);

            if (!_context.ChangeTracker.HasChanges())
                return true;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return false;
        }
    }

    public async Task<bool> RemoveAttack(long pokemon_id, long attack_id)
    {
        try
        {
            Pokemon pokemonEntity = _set.Include("Attacks").First(p => p.Id == pokemon_id) ?? throw new DataException($"Entity of type Pokemon not found with id {pokemon_id}");
            Attack attackEntity = _attacksSet.Find(attack_id) ?? throw new DataException($"Entity of type Attack not found with id {pokemon_id}");

            if (pokemonEntity.Attacks.Any(a => a.Id == attack_id))
                pokemonEntity.Attacks.Remove(attackEntity);

            if (!_context.ChangeTracker.HasChanges())
                return true;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return false;
        }
    }
}
