using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    public class ThumbnailDataController : ODataController
    {
        private readonly IGalleryService galleryService;

        public ThumbnailDataController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        [EnableQuery]
        public async Task<ActionResult<ThumbnailDataDto>> Get([FromODataUri] int key)
        {
            var thumbnailData = await galleryService.GetThumbnailDataAsync(key);
            return thumbnailData is not null ? Ok(thumbnailData) : NotFound();
        }
    }
}
