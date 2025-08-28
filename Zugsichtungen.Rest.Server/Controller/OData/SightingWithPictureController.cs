using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    [Route("odata/[controller]")]
    public class SightingWithPictureController : ODataController
    {
        private readonly ISightingService sightingService;

        public SightingWithPictureController(ISightingService sightingService)
        {
            this.sightingService = sightingService;
        }

        public async Task<ActionResult> Post([FromBody] SightingWithPictureDto input)
        {
            await sightingService.AddSightingAsync(input.Sighting, input.Picture);
            return NoContent();
        }
    }
}
