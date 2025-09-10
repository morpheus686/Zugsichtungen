using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Rest.Server.Hubs;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    public class SightingController : ODataController
    {
        private readonly ISightingService sightingService;
        private readonly IHubContext<SightingHub> sightingHub;

        public SightingController(ISightingService service, IHubContext<SightingHub> sightingHub)
        {
            this.sightingService = service;
            this.sightingHub = sightingHub;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<SightingViewEntryDto>>> Get()
        {
            var entries = await sightingService.GetAllSightingViewEntriesAsync();
            return Ok(entries.AsQueryable());
        }

        public async Task<ActionResult> Post([FromBody] SightingWithPictureDto input)
        {
            var newSightingId = await sightingService.AddSightingAsync(input.Sighting, input.Picture);
            var savedDto = await sightingService.GetSightingViewEntryBySightingIdAsync(newSightingId);
            await this.sightingHub.Clients.All.SendAsync("SightingAdded", savedDto);
            return Ok();
        }
    }
}
