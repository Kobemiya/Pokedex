using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokedexBackend.DataAccess.EfModels;
using System.Data;

namespace PokedexBackend.DataAccess.Repositories;

public class UsersRepository : CRUDRepository<User, Dbo.User>, IUsersRepository
{
    protected readonly DbSet<Pokemon> _pokemonsSet;

    public UsersRepository(PokedexDotNetContext context, ILogger<CRUDRepository<User, Dbo.User>> logger, IMapper mapper) :
        base(context, logger, mapper)
    {
        _pokemonsSet = _context.Set<Pokemon>();
    }

    public async Task<bool> AddFavorite(long user_id, long pokemon_id)
    {
        try
        {
            User userEntity = _set.Include("Pokemons").First(p => p.Id == user_id) ?? throw new DataException($"Entity of type User not found with id {user_id}");
            Pokemon pokemonEntity = _pokemonsSet.Find(pokemon_id) ?? throw new DataException($"Entity of type Pokemon not found with id {pokemon_id}");

            if (!userEntity.Pokemons.Any(a => a.Id == pokemon_id))
                userEntity.Pokemons.Add(pokemonEntity);

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

    public async Task<bool> RemoveFavorite(long user_id, long pokemon_id)
    {
        try
        {
            User userEntity = _set.Include("Pokemons").First(p => p.Id == user_id) ?? throw new DataException($"Entity of type User not found with id {user_id}");
            Pokemon pokemonEntity = _pokemonsSet.Find(pokemon_id) ?? throw new DataException($"Entity of type Pokemon not found with id {pokemon_id}");

            if (userEntity.Pokemons.Any(a => a.Id == pokemon_id))
                userEntity.Pokemons.Remove(pokemonEntity);

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
