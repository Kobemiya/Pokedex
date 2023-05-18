using AutoMapper;

namespace WebApp.DataAccess;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<EfModels.TShortcut, Dbo.Shortcut>();
        CreateMap<Dbo.Shortcut, EfModels.TShortcut>();
        CreateMap<EfModels.TStat, Dbo.Stat>();
        CreateMap<Dbo.Stat, EfModels.TStat>();
        CreateMap<EfModels.StatByUrl, Dbo.StatByUrl>();
        CreateMap<Dbo.StatByUrl, EfModels.StatByUrl>();
    }
}