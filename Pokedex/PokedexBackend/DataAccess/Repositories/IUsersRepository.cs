using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public interface IUsersRepository : ICRUDRepository<User, Dbo.User> {}
