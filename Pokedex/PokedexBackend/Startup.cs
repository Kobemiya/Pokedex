using PokedexBackend.DataAccess;
using PokedexBackend.DataAccess.EfModels;
using PokedexBackend.DataAccess.Repositories;

namespace PokedexBackend;

public static class Startup 
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSession();
        services.AddLogging();
        services.AddControllers();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPokemonsRepository, PokemonsRepository>();
        services.AddScoped<IAttacksRepository, AttacksRepository>();
        services.AddEntityFrameworkSqlServer();
        services.AddDbContext<PokedexDotNetContext>();
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    public static void ConfigureApplication(WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) 
            app.UseExceptionHandler("/Error"); 

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseSession();
        app.MapControllers();
        app.MapRazorPages();
    }
}