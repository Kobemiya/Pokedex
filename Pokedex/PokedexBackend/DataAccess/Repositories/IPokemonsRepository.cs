using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public interface IPokemonsRepository : ICRUDRepository<Pokemon, Dbo.Pokemon> {}
