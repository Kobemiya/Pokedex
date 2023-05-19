using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public interface IUsersRepository : ICRUDRepository<User, Dbo.User>
{
    Task<bool> AddFavorite(long user_id, long pokemon_id);
    Task<bool> RemoveFavorite(long user_id, long pokemon_id);
}
