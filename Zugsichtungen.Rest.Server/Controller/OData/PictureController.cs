using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    public class PictureController : ODataController
    {
        private readonly IGalleryService galleryService;

        public PictureController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<PictureDto>>> Get()
        {
            var gallerypictures = await galleryService.GetGalleryPicturesAsync();
            return Ok(gallerypictures.AsQueryable());
        }

    }
}
