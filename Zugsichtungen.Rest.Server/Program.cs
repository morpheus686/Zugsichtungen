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
builder.Services.AddScoped<IGalleryService, GalleryService>();
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
    modelBuilder.EntitySet<VehicleViewEntryDto>("VehicleView");
    modelBuilder.EntitySet<VehicleDto>("Vehicle");
    modelBuilder.EntitySet<SeriesDto>("Series");  
    modelBuilder.EntitySet<ThumbnailDataDto>("ThumbnailData");
    // PictureDto comes from a view and ODataConventionModelBuilder may not detect a key.
    // Define the key explicitly so the EDM model can be built.
    modelBuilder.EntitySet<PictureDto>("Picture").EntityType.HasKey(p => new { p.PictureId, p.SightingId});

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

    app.MapPost("api/addsightingwithpicture", async (SightingWithPictureDto sightingWithPicture, ISightingService service, IHubContext<SightingHub> hub) =>
    {
        var newSightingId = await service.AddSightingAsync(sightingWithPicture);
        var savedDto = await service.GetSightingViewEntryBySightingIdAsync(newSightingId);
        await hub.Clients.All.SendAsync("SightingAdded", savedDto);
    });

    app.MapGet("api/vehicleview", async (ISightingService service) =>
    {
        var entries = await service.GetVehicleViewEntriesAsync();
        return Results.Ok(entries);
    });

    app.MapGet("api/contexts", async (ISightingService service) =>
    {
        var entries = await service.GetContextsAsync();
        return Results.Ok(entries);
    });

    app.MapGet("api/sightingpicture", async (int sightingId, ISightingService service) =>
    {
        var picture = await service.GetSightingPictureBySightingIdAsync(sightingId);
        return picture is not null ? Results.Ok(picture) : Results.NotFound();
    });

    app.MapGet("api/allseries", async (ISightingService service) =>
    {
        var entries = await service.GetAllSeriesAsync();
        return Results.Ok(entries);
    });

    app.MapGet("api/allvehicles", async (ISightingService service) =>
    {
        var entries = await service.GetAllVehiclesAsync();
        return Results.Ok(entries);
    });

    app.MapGet("api/pictures", async (IGalleryService service) =>
    {
        var entries = await service.GetGalleryPicturesAsync();
        return Results.Ok(entries);
    });

    app.MapGet("api/thumbnail", async (int pictureId, IGalleryService service) =>
    {
        var thumbnailData = await service.GetThumbnailDataAsync(pictureId);
        return thumbnailData is not null ? Results.Ok(thumbnailData) : Results.NotFound();
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

    builder.Services.AddScoped<ISightingDataService, SQLiteSightingDataService>();
    builder.Services.AddScoped<IGalleryDataService, SQLiteGalleryDataService>();
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

//    builder.Services.AddScoped<ISightingDataService, SqlServerSightingDataService>();
//    builder.Services.AddScoped<IGalleryDataService, SqlServerGalleryDataService>();
//}