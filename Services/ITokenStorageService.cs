namespace DRproxy.Services;

public interface ITokenStorageService
{

    public  Task<String?> Get();

}