using System.Collections.Concurrent;

namespace DRproxy.MemoryStorage;

public class ConnectionMapping<T>
{
    private readonly ConcurrentDictionary<string, string> _clientToConnection = new();
    private readonly ConcurrentDictionary<string, string> _connectionToClient = new();

    public int Count => _clientToConnection.Count;

    public void Add(string clientId, string connectionId)
    {
        _clientToConnection[clientId] = connectionId;
        _connectionToClient[connectionId] = clientId;
    }

    public string GetClientId(string connectionId)
    {
        return _connectionToClient.TryGetValue(connectionId, out var clientId) ? clientId : "";
    }

    public string GetConnectiontId(string clientId)
    {
        return _clientToConnection.TryGetValue(clientId, out var connectionId) ? connectionId : "";
    }

    public void Remove(string connectionId)
    {
        if (_connectionToClient.TryRemove(connectionId, out var clientId))
        {
            _clientToConnection.TryRemove(clientId, out _);
        }
    }
}