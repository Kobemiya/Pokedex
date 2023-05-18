using PokedexBackend;

var builder = WebApplication.CreateBuilder(args);

Startup.ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();

Startup.ConfigureApplication(app);

app.Run();
