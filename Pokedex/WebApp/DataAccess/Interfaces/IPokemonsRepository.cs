using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.EfModels;

namespace WebApp.DataAccess.Interfaces;

public interface IPokemonsRepository : IRepository<TPokemons, Pokemon>
{
    public Pokemon? GetByHash(string hash);
    public IEnumerable<Pokemon> GetBySessionId(string sessionId);
}