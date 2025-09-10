using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    public class SightingController : ODataController
    {
        private readonly ISightingService service;

        public SightingController(ISightingService service)
        {
            this.service = service;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<SightingViewEntryDto>>> Get()
        {
            var entries = await service.GetAllSightingViewEntriesAsync();
            return Ok(entries.AsQueryable());
        }
    }
}
