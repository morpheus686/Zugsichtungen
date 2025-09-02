using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Rest.Server.Hubs;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    [Route("odata/[controller]")]
    public class SightingWithPictureController : ODataController
    {
        private readonly ISightingService sightingService;
        private readonly SightingHub sightingHub;

        public SightingWithPictureController(ISightingService sightingService, SightingHub sightingHub)
        {
            this.sightingService = sightingService;
            this.sightingHub = sightingHub;
        }

        public async Task<ActionResult> Post([FromBody] SightingWithPictureDto input)
        {
            var newSightingId = await sightingService.AddSightingAsync(input.Sighting, input.Picture);
            var savedDto = await sightingService.GetSightingViewByIdAsync(newSightingId);
            await this.sightingHub.Clients.All.SendAsync("SightingAdded", savedDto);
            return NoContent();
        }
    }
}
