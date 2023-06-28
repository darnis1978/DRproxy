using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DRproxy.MemeoryStorage;

namespace DRproxy.Hubs {


    public class MessageHub: Hub<IMessageHubClient> {
       
        private readonly static ConnectionMapping<string> _connections = 
            new ConnectionMapping<string>();


        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RegisterClient(string clientId) {
            
            Console.WriteLine("----------------------------------");
            Console.WriteLine(Context.ConnectionId + " POS: " + clientId);
            Console.WriteLine("----------------------------------");
            _connections.Add(clientId, Context.ConnectionId);
        }

        public async Task SendResponseToClient(string message, string clientId) 
        {
            //  await Clients.Client(_connections.GetConnectiontId(clientId)).SendAsync("ReceiveMessage", message);
        }
    }
}