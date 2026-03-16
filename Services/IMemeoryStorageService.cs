namespace DRproxy.Services;

public interface IMemeoryStorageService
{
    public void Add(string clientId, string connectionId);
    public String GetClientId(string connectionId);
    public String GetConnectiontId(string clientId);
    public void Remove(string connectionId);
    public int Count {get;}
}