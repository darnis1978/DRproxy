using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DRproxy.Services;
using System.Text.Json.Nodes;

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

    [HttpPost("Receipt")]
    public async Task<ActionResult<DRfiscalResponse>>Receipt([FromBody] JsonObject json)
    {   
        // Process transaction files
        DRfiscalResponse res = await _digitalReceipt.ProcessTransaction(json);
        if (res.ErrorCode!= null){
            return NotFound(res);   
        }

        return Ok(res);
    }
}
