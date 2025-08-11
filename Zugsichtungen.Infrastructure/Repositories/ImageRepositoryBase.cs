using System.Data.Common;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Infrastructure.Repositories
{
    public abstract class ImageRepositoryBase : IImageRepository
    {
        public abstract Task<bool> CheckIfImageExistsAsync(int sightingId);
        public abstract Task<SightingPictureDto?> GetImageBySightingIdAsync(int sightingId);
    }
}
