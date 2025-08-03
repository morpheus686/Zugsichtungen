using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class VehicleViewEntryProfile : Profile
    {
        public VehicleViewEntryProfile()
        {
            CreateMap<VehicleViewEntryDto, VehicleViewEntry>();
        }
    }
}
