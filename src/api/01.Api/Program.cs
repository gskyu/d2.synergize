using _02.Application.Services;
using _03.Infrastructure.Apis;
using _03.Infrastructure.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBungieService>();
builder.Services.AddScoped<IReferencesService>();

builder.Services.AddMemoryCache();
builder.Services.AddRefitClient<IBungieApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://www.bungie.net");
        // TODO: Add support for dotnet user secrets and setup local asp net core env
        c.DefaultRequestHeaders.Add("X-API-Key", "TODO");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
        "/weatherforecast", 
        async (IReferencesService referencesService, CancellationToken cancellationToken) 
            => await referencesService.GetSubClassReferencesAsync(cancellationToken))
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();