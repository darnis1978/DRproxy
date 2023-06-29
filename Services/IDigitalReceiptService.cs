using System.Text.Json;

namespace DRproxy.Services;

public interface IDigitalReceiptService
{
    public Task<DRfiscalResponse> processTransaction(JsonDocument txt);
}

