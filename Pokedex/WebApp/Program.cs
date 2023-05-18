using WebApp;

var builder = WebApplication.CreateBuilder(args);

Startup.ConfigureServices(builder.Services);

var app = builder.Build();

Startup.ConfigureApplication(app);

app.Run();
