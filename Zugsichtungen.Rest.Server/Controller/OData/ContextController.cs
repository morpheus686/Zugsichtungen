using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Rest.Server.Controller.OData
{
    [Route("odata/[controller]")]
    public class ContextController : ODataController
    {
        private readonly ISightingService service;

        public ContextController(ISightingService service)
        {
            this.service = service;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<ContextDto>>> Get()
        {
            var entries = await service.GetContextesAsync();
            return Ok(entries.AsQueryable());
        }
    }
}
