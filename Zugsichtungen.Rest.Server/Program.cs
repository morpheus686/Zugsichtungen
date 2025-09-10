using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Helpers;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Repositories;
using Zugsichtungen.Infrastructure.SQLite.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;
using Zugsichtungen.Infrastructure.SQLServer.Repositories;
using Zugsichtungen.Infrastructure.SQLServer.Services;
using Zugsichtungen.Rest.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

AddOData(builder);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

UseSqlite(builder);
//UseSqlServer(builder);

builder.Services.AddScoped<ISightingService, SightingService>();
builder.Services.AddAutoMapper(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
AddSignalR(builder);

var app = builder.Build();

app.UseCors();
app.MapHub<SightingHub>("/SignalRHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.MapControllers();

MapMinimalApi(app);

app.Run("http://0.0.0.0:7046");

static void AddSignalR(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<SightingHub>();

    builder.Services.AddSignalR();
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    });
}

static void AddOData(WebApplicationBuilder builder)
{
    builder.Services.AddOData();
    var modelBuilder = new ODataConventionModelBuilder();
    modelBuilder.EntitySet<SightingViewEntryDto>("Sighting");
    modelBuilder.EntitySet<SightingPictureDto>("SightingPicture");
    modelBuilder.EntitySet<ContextDto>("Context");
    modelBuilder.EntitySet<VehicleViewEntryDto>("Vehicle");

    builder.Services.AddControllers().AddOData(
        options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
            "odata",
            modelBuilder.GetEdmModel()));
}

static void MapMinimalApi(WebApplication app)
{
    app.MapGet("api/sightings", async (ISightingService service) =>
    {
        var entries = await service.GetAllSightingViewEntriesAsync();
        return Results.Ok(entries);
    });

    app.MapPost("api/addsighting", async (Tuple<SightingDto, SightingPictureDto> sighting, ISightingService service, IHubContext<SightingHub> hub) =>
    {
        var newSightingId = await service.AddSightingAsync(sighting.Item1, sighting.Item2);
        var savedDto = await service.GetSightingViewByIdAsync(newSightingId);
        await hub.Clients.All.SendAsync("SightingAdded", savedDto);
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
}

static void UseSqlite(WebApplicationBuilder builder)
{
    var dbPath = SqliteHelper.CopyDatabaseIfNotExits();
    var sqliteConnectionString = $"Data Source={dbPath}";


    builder.Services.AddDbContext<ZugbeobachtungenContext>(options =>
    {
        options.UseSqlite(sqliteConnectionString);
    });
    builder.Services.AddScoped<IImageRepository, SQLiteImageRepository>(sp =>
    {
        return new SQLiteImageRepository(sqliteConnectionString);
    });

    builder.Services.AddScoped<IDataService, SQLiteDataService>();
}

//static void UseSqlServer(WebApplicationBuilder builder)
//{
//    var sqlserverConnectionstring = "Data Source=Christopher-PC\\SQLEXPRESS01;Initial Catalog=Trainspotting;Integrated Security=True;Trust Server Certificate=True";

//    builder.Services.AddDbContext<TrainspottingContext>(options =>
//    {
//        options.UseSqlServer(sqlserverConnectionstring);
//    });

//    builder.Services.AddScoped<IImageRepository, SQLServerImageRepository>(sp =>
//    {
//        return new SQLServerImageRepository(sqlserverConnectionstring);
//    });

//    builder.Services.AddScoped<IDataService, SqlServerDataService>();
//}