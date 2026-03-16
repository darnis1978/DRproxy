using Microsoft.AspNetCore.Mvc;
using DRproxy.Services;
using System.Text.Json.Nodes;
using anybill.POS.Client.Response.Bill;

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
    public async Task<ActionResult>Receipt([FromBody] JsonObject json)
    {   
        ActionResult response = await _digitalReceipt.ProcessTransaction(json) ;
        return response;
    }

}
