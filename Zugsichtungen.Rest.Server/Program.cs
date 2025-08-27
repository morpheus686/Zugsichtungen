using Microsoft.EntityFrameworkCore;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Repositories;
using Zugsichtungen.Infrastructure.SQLite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = "Data Source=zugbeobachtungen.db";

builder.Services.AddDbContext<ZugbeobachtungenContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IDataService, SQLiteDataService>();

if (connectionString == null)
{
    throw new ApplicationException("Connectionstring ist nicht in den Einstellungen festgelegt!");
}

builder.Services.AddScoped<IImageRepository, SQLiteImageRepository>(sp =>
{
    return new SQLiteImageRepository(connectionString);
});

builder.Services.AddScoped<ISightingService, SightingService>();
builder.Services.AddAutoMapper(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//app.UseAuthorization();

//app.MapControllers();

app.MapGet("api/sightings", async (ISightingService service) =>
{
    var entries = await service.GetAllSightingViewEntriesAsync();
    return Results.Ok(entries);
});

app.MapPost("api/addsighting", async (Tuple<SightingDto, SightingPictureDto> sighting, ISightingService service) =>
{
    await service.AddSightingAsync(sighting.Item1, sighting.Item2);
});

app.MapGet("api/vehicleview", async (ISightingService service) =>
{
    var entries = await service.GetVehicleViewEntriesAsync();
    return Results.Ok(entries);
});

app.MapGet("api/contexts", async (ISightingService service) =>
{
    var entries = await service.GetContextesAsync();
    return Results.Ok(entries);
});

app.MapGet("api/sightingpicture", async (int sightingId, ISightingService service) =>
{
    var picture = await service.GetPictureBySightingIdAsync(sightingId);
    return picture is not null ? Results.Ok(picture) : Results.NotFound();
});

app.Run();
