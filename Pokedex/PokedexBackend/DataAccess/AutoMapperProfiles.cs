using AutoMapper;

namespace PokedexBackend.DataAccess;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<EfModels.User, Dbo.User>();
        CreateMap<Dbo.User, EfModels.User>();
        CreateMap<EfModels.Pokemon, Dbo.Pokemon>();
        CreateMap<Dbo.Pokemon, EfModels.Pokemon>();
        CreateMap<EfModels.Attack, Dbo.Attack>();
        CreateMap<Dbo.Attack, EfModels.Attack>();
    }
}