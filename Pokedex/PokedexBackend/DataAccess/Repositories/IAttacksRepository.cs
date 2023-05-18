using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public interface IAttacksRepository : ICRUDRepository<Attack, Dbo.Attack> {}
