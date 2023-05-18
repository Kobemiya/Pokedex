using AutoMapper;
using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public class AttacksRepository : CRUDRepository<Attack, Dbo.Attack>, IAttacksRepository
{
    public AttacksRepository(PokedexDotNetContext context, ILogger<CRUDRepository<Attack, Dbo.Attack>> logger, IMapper mapper) :
        base(context, logger, mapper)
    {}
}
