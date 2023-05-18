using WebApp.DataAccess;
using WebApp.DataAccess.EfModels;
using WebApp.DataAccess.Interfaces;

namespace WebApp;

public static class Startup 
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSession();
        services.AddLogging();
        services.AddControllers();
        services.AddScoped<IShortcutsRepository, ShortcutsRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();
        services.AddEntityFrameworkSqlServer();
        services.AddDbContext<PokedexContext>();
        services.AddAutoMapper(typeof(AutoMapperProfiles));
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