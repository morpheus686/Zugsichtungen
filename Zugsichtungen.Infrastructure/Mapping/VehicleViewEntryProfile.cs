using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Sighting;

namespace Zugsichtungen.Infrastructure.Mapping
{
    internal class VehicleViewEntryProfile : Profile
    {
        public VehicleViewEntryProfile()
        {
            CreateMap<VehicleViewEntry, VehicleViewEntryDto>();
        }
    }
}
