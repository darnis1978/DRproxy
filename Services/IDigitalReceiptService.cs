using System.Text.Json;
using System.Text.Json.Nodes;

namespace DRproxy.Services;

public interface IDigitalReceiptService
{
    public Task<DRfiscalResponse> ProcessTransaction(JsonObject txt);
}

