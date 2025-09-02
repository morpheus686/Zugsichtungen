using Microsoft.AspNetCore.SignalR;
using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Rest.Server.Hubs
{
    public class SightingHub : Hub
    {
        public async Task BroadcastSighting(SightingViewEntryDto sighting)
        {
            await Clients.All.SendAsync("SightingAdded", sighting);
        }
    }
}
