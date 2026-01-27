using Zugsichtungen.Domain.Models.Gallery;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IGalleryDataService
    {
        Task<List<Picture>> GetPicturesAsync();
    }
}
