using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Domain.Models;
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
            IImageRepository imageRepository) : base(context)
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

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            this.logger.LogInformation("Rufe alle Themen aus der Datenbank ab.");
            var contextList = await context.Contexts.ToListAsync();
            return mapper.MapList<Models.Context, ContextDto>(contextList);
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

        public async override Task AddAsync(Domain.Models.Sighting sighting)
        {
            var entity = MapToEntity(sighting);
            await this.context.Sightings.AddAsync(entity);
            await SaveChangesAsync();
        }

        private Models.Sighting MapToEntity(Domain.Models.Sighting sighting)
        {
            var entity = new Models.Sighting
            {
                Date = sighting.Date,
                Location = sighting.Location,
                VehicleId = sighting.VehicleId,
                ContextId = sighting.ContextId,
                Comment = sighting.Note
            };

            var sightingPicture = sighting.SightingPicture;

            if (sightingPicture != null)
            {
                entity.SightingPictures.Add(new Models.SightingPicture
                {
                    Image = sightingPicture.Image,
                    Filename = sightingPicture.Filename
                });
            }

            return entity;
        }

        public async override Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync()
        {
            var sichtungen = await context.SightingLists.ToListAsync();
            var sightingList = new List<SightingViewEntry>();

            foreach (var item in sichtungen)
            {
                sightingList.Add(MapFromEntity(item));
            }

            return sightingList;
        }

        private SightingViewEntry MapFromEntity(SightingList entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.SightingDate, entity.VehicleNumber, entity.Location, null, entity.Comment, null);
        }
    }
}
