using WebCatalogOfFootballers.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebCatalogOfFootballers.Hubs
{
    public class FootballersHub : Hub
    {
        public async Task Sender(Footballer model)
            => await this.Clients.All.SendAsync("Receive", model);

        public async Task Editor(Footballer model)
            => await this.Clients.All.SendAsync("ReceiveEdit", model);

        public async Task Deleter(string id)
            => await this.Clients.All.SendAsync("ReceiveDelete", id);
    }
}