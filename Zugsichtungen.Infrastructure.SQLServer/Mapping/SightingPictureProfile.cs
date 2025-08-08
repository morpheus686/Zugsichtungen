using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Mapping
{
    public class SightingPictureProfile : Profile
    {
        public SightingPictureProfile() 
        {
            CreateMap<SightingPictureDto, SightingPicture>().ReverseMap();
        }
    }
}
