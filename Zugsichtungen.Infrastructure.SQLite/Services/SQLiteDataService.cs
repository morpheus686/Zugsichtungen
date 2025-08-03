using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Foundation.Mapping;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Services
{
    public class SQLiteDataService : DataServiceBase
    {
        private readonly ZugbeobachtungenContext context;
        private readonly IMapper mapper;

        public SQLiteDataService(ZugbeobachtungenContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await context.Fahrzeuglistes.ToListAsync();
            return mapper.MapList<Fahrzeugliste, VehicleViewEntryDto>(fahrzeuge);
        }

        public override async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.Sichtungsviews.ToListAsync();
            return mapper.MapList<Sichtungsview, SightingViewEntryDto>(sichtungen);
        }

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Kontextes.ToListAsync();
            return mapper.MapList<Kontexte, ContextDto>(kontexte);
        }

        public override async Task AddSichtungAsync(SightingDto newSichtung)
        {
            await context.Sichtungens.AddAsync(mapper.MapSingle<SightingDto, Sichtungen>(newSichtung));
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }
    }
}
