using AutoMapper;

namespace WebApp.DataAccess;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<EfModels.TPokemons, Dbo.Pokemon>();
        CreateMap<Dbo.Pokemon, EfModels.TPokemons>();
        CreateMap<EfModels.TStat, Dbo.Stat>();
        CreateMap<Dbo.Stat, EfModels.TStat>();
        CreateMap<EfModels.StatByUrl, Dbo.StatByUrl>();
        CreateMap<Dbo.StatByUrl, EfModels.StatByUrl>();
    }
}