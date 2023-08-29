using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace DRproxy.Services;

public interface IDigitalReceiptService
{
    // public Task<T> PrepareTransaction<T>(JsonObject json);

    // This function is called by controlled. 
    // It needs to return ActionResult object.
    public Task<ActionResult> ProcessTransaction(JsonObject json);
}

