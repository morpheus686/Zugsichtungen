using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    [Route("odata/[controller]")]
    public class VehicleController : ODataController
    {
        private readonly ISightingService sightingService;

        public VehicleController(ISightingService sightingService)
        {
            this.sightingService = sightingService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<VehicleViewEntryDto>>> Get()
        {
            var entries = await sightingService.GetVehicleViewEntriesAsync();
            return Ok(entries.AsQueryable());
        }
    }
}
