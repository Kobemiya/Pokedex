using AutoMapper;
using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.EfModels;
using WebApp.DataAccess.Interfaces;

namespace WebApp.DataAccess;

public class PokemonsRepository : Repository<EfModels.TPokemons, Dbo.Pokemon>, IPokemonsRepository
{
    public PokemonsRepository(PokedexContext context, ILogger<PokemonsRepository> logger, IMapper mapper) : base(context, logger, mapper) { }
    public Pokemon? GetByHash(string hash)
    {
        return _mapper.Map<Pokemon?>(_context.TPokemons.FirstOrDefault(s => s.Hash == hash));
    }

    public IEnumerable<Pokemon> GetBySessionId(string sessionId)
    {
        return _context.TPokemons
            .Where(s => s.SessionId == sessionId)
            .Select(s => _mapper.Map<Pokemon>(s));
    }
}