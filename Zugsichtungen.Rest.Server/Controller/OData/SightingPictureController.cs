using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    [Route("odata/[controller]")]
    public class SightingPictureController : ODataController
    {
        private readonly ISightingService service;

        public SightingPictureController(ISightingService service)
        {
            this.service = service;
        }

        [EnableQuery]
        public async Task<ActionResult<SightingPictureDto>> Get([FromODataUri] int key) 
        {
            var picture = await service.GetPictureBySightingIdAsync(key);
            return picture is not null ? Ok(picture) : NotFound();
        }
    }
}
