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
    private IHubContext < MessageHub, IMessageHubClient > _messageHub;


    public ProxyController(ILogger<ProxyController> logger, IHubContext < MessageHub, IMessageHubClient > messageHub)
    {
        _logger = logger;
        _messageHub = messageHub;

    }

    [HttpPost("receipt")]
    public ActionResult<String> receipt([FromBody] JsonDocument txt)
    { 

        Dictionary<string, object> payload = JsonSerializer.Deserialize<Dictionary<string, object>>(txt);
        
        Object posId;
        payload!.TryGetValue("PosNmbr" , out posId);
        // _messageHub.Clients.All.SendAsync("ReceiveMessage", "this is DR feedbeck" );
        _logger.LogInformation(posId?.ToString());
        _messageHub.Clients.All.SendResponseToClient(txt.ToString(), posId?.ToString());

        return Ok(txt);
    }
}
