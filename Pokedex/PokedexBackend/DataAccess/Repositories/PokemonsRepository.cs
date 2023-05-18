using AutoMapper;
using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public class PokemonsRepository : CRUDRepository<Pokemon, Dbo.Pokemon>, IPokemonsRepository
{
    public PokemonsRepository(PokedexDotNetContext context, ILogger<CRUDRepository<Pokemon, Dbo.Pokemon>> logger, IMapper mapper) :
        base(context, logger, mapper)
    {}
}
