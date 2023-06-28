using Microsoft.AspNetCore.SignalR;

namespace DRproxy.Hubs {
    public interface IMessageHubClient {
        Task SendResponseToClient(string message, string clientId);
    }
}