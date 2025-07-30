using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.Models;

namespace Zugsichtungen.Extensions
{
    public static class Mapper
    {
        public static SightingViewEntryDto ToDto(this Sichtungsview entity)
        {
            return new SightingViewEntryDto
            {
                Id = entity.Id,
                VehicleNumber = entity.Loknummer,
                Location = entity.Ort,
                Date = entity.Datum,
                Note = entity.Bemerkung
            };
        }

        public static VehicleViewEntryDto ToDto(this Fahrzeugliste entity)
        {
            return new VehicleViewEntryDto
            {
                Id = entity.Id,
                Vehicle = entity.Fahrzeug
            };
        }

        public static ContextDto ToDto(this Kontexte entity)
        {
            return new ContextDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static Sichtungen FromDto(this SightingDto dto)
        {
            return new Sichtungen
            {
                KontextId = dto.ContextId,
                FahrzeugId = dto.VehicleId,
                Ort = dto.Location,
                Datum = dto.Date,
                Bemerkung = dto.Note
            };
        }
    }
}
