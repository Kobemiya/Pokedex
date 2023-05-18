using AutoMapper;
using PokedexBackend.DataAccess.EfModels;

namespace PokedexBackend.DataAccess.Repositories;

public class UsersRepository : CRUDRepository<User, Dbo.User>, IUsersRepository
{
    public UsersRepository(PokedexDotNetContext context, ILogger<CRUDRepository<User, Dbo.User>> logger, IMapper mapper) :
        base(context, logger, mapper)
    {}
}
