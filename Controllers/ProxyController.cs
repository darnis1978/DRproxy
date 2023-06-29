using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DRproxy.Services;

namespace DRproxy.Controllers;

[ApiController]
[Route("api")]
public class ProxyController : ControllerBase
{
    private readonly ILogger<ProxyController> _logger;

    private readonly IDigitalReceiptService _digitalReceipt;


    public ProxyController( ILogger<ProxyController> logger, 
                            IDigitalReceiptService digitalReceipt
                        )
    {
        _logger = logger;
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
