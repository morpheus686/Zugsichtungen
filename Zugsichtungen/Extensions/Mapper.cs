using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Models;

namespace Zugsichtungen.Extensions
{
    public static class Mapper
    {
        public static SightingViewEntry ToDto(this Sichtungsview entity)
        {
            return new SightingViewEntry
            {
                Id = entity.Id,
                VehicleNumber = entity.Loknummer,
                Location = entity.Ort,
                Date = entity.Datum,
                Note = entity.Bemerkung
            };
        }

        public static VehicleViewEntry ToDto(this Fahrzeugliste entity)
        {
            return new VehicleViewEntry
            {
                Id = entity.Id,
                Vehicle = entity.Fahrzeug
            };
        }

        public static Context ToDto(this Kontexte entity)
        {
            return new Context
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static Sichtungen FromDto(this Sighting dto)
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
