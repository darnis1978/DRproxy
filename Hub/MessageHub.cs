using Microsoft.AspNetCore.SignalR;
using DRproxy.Services;

namespace DRproxy.Hubs;

public class MessageHub: Hub {
    
    private readonly IMemeoryStorageService _connections; 

    public MessageHub(IMemeoryStorageService ms){
        _connections = ms;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var _clientId = _connections.GetClientId(Context.ConnectionId);
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("Disconnected client,      [POS: " + _clientId + "] [Connection ID: " + Context.ConnectionId + "]" );
        _connections.Remove(Context.ConnectionId);
        
        if (_connections.Count == 0) {
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("All clients disconnected !" );
        }

        return base.OnDisconnectedAsync(exception);
    }

    public void RegisterClient(string clientId) {
        
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("New client registered,    [POS: " + clientId + "] [Connection ID: " + Context.ConnectionId + "]" );
        _connections.Add(clientId, Context.ConnectionId);
    }
}