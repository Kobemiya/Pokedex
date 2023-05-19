using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public interface IPokemonsRepository : ICRUDRepository<Pokemon, Dbo.Pokemon>
{
    Task<bool> AddAttack(long pokemon_id, long attack_id);
    Task<bool> RemoveAttack(long pokemon_id, long attack_id);
}
