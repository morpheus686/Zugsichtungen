using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Foundation.Mapping;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Services
{
    public class SqlServerDataService : DataServiceBase
    {
        private readonly TrainspottingContext context;
        private readonly IMapper mapper;
        private readonly ILogger<SqlServerDataService> logger;
        private readonly IImageRepository imageRepository;

        public SqlServerDataService(TrainspottingContext context,
            IMapper mapper, 
            ILogger<SqlServerDataService> logger,
            IImageRepository imageRepository) : base(context, logger, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.imageRepository = imageRepository;
        }

        public override async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            this.logger.LogInformation("Rufe alle Triebfahrzeuge aus der Datenbank ab.");
            var vehicleList = await context.Vehiclelists.ToListAsync();
            return mapper.MapList<Vehiclelist, VehicleViewEntryDto>(vehicleList);
        }

        public override async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            this.logger.LogInformation("Rufe alle Sichtungen aus der Datenbank ab.");
            var sichtungen = await context.SightingLists.ToListAsync();
            return mapper.MapList<SightingList, SightingViewEntryDto>(sichtungen);
        }

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            this.logger.LogInformation("Rufe alle Themen aus der Datenbank ab.");
            var contextList = await context.Contexts.ToListAsync();
            return mapper.MapList<Context, ContextDto>(contextList);
        }

        public override Task AddSightingAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto)
        {
            return AddSightingInternalAsync(newSichtung, sightingPictureDto, context.Sightings, context.SightingPictures, "Sighting");
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        public override Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId)
        {
            return this.imageRepository.GetImageBySightingIdAsync(sightingId);
        }

        public override Task<bool> CheckIfSightingPictureExists(int sightingId)
        {
            return this.imageRepository.CheckIfImageExistsAsync(sightingId);
        }

        public override Task<bool> DeleteSightingAsync(int sightingId)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SightingPictureDto>> GetAllSightingPicturesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
