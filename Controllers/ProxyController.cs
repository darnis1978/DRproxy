using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using DRproxy.Hubs;
using System.Text.Json;

namespace DRproxy.Controllers;

[ApiController]
[Route("api")]
public class ProxyController : ControllerBase
{
    private readonly ILogger<ProxyController> _logger;
    // private IHubContext < MessageHub > _messageHub;
    // private readonly IMemeoryStorageService _connections; 

    private readonly IDigitalReceiptService _digitalReceipt;


    public ProxyController( ILogger<ProxyController> logger, 
                            // IHubContext < MessageHub > messageHub, 
                            // IMemeoryStorageService connections,
                            IDigitalReceiptService digitalReceipt
                        )
    {
        _logger = logger;
        // _messageHub = messageHub;
        // _connections = connections;
        _digitalReceipt = digitalReceipt;

    }

    [HttpPost("receipt")]
    public async Task<ActionResult<DRfiscalResponse>> receipt([FromBody] JsonDocument txt)
    { 
    
        // Process transaction files
        DRfiscalResponse res = await _digitalReceipt.processTransaction(txt);
        if (res._errorCode!= null){
            return NotFound(res);   
        }

        return Ok(res);
    }
}
