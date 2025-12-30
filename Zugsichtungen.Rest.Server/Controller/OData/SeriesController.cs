using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    public class SeriesController : ODataController
    {
        private readonly ISightingService sightingService;

        public SeriesController(ISightingService sightingService) 
        {
            this.sightingService = sightingService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<SeriesDto>>> Get()
        {
            var entries = await sightingService.GetAllSeriesAsync();
            return Ok(entries.AsQueryable());
        }
    }
}
