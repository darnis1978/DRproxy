using DRproxy.MemoryStorage;

namespace DRproxy.Services;

public class MemeoryStorageService: IMemeoryStorageService
{    
    private readonly ConnectionMapping<string> _connections= new ConnectionMapping<string>();
    
    public void Add(string clientId, string connectionId)
    {
        _connections.Add(clientId, connectionId );
    }

    public String GetClientId(string connectionId)
    {
        return _connections.GetClientId(connectionId );
    }

    public String GetConnectiontId(string clientId)
    {
        return _connections.GetConnectiontId(clientId );
    }

    public void Remove(string connectionId)
    {
        _connections.Remove(connectionId);
    }

    public int Count
    {
        get
            {
                return _connections.Count;
            }
    }
    
}