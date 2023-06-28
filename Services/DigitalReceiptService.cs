using DRproxy.Controllers;
using DRproxy.MemeoryStorage;
using Microsoft.AspNetCore.SignalR;
using DRproxy.Hubs;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public interface IDigitalReceiptService
{
    public Task<DRfiscalResponse> processTransaction(JsonDocument txt);

}

public class DRfiscalResponse {
    public int? _errorCode { get; set; }
    public string? _UID { get; set; }

}

public class DRclientResponse {
    
    public string? _clientId { get; set; }
    public string? _QRcode { get; set; }
}

public class DigitalReceiptService: IDigitalReceiptService
{    
       
       private readonly IMemeoryStorageService _connectionsPool; 
       private IHubContext <MessageHub> _messageHub;


        public DigitalReceiptService(
                                    IHubContext < MessageHub > messageHub, 
                                    IMemeoryStorageService connectionPool
                                    ){

            _connectionsPool = connectionPool;
            _messageHub = messageHub;
        }

         public async Task<DRfiscalResponse> processTransaction(JsonDocument txt){
       
    
            Dictionary<string, object>? payload = JsonSerializer.Deserialize<Dictionary<string, object>>(txt);
            if (payload == null){       
                return new DRfiscalResponse() { _errorCode= 1, _UID= null } ;
            }
        
            // get client id from TX
            Object? _clientId; 
            payload!.TryGetValue("PosNmbr" , out _clientId);

            // get connection id from connections pool
            string _connectionId = _connectionsPool.GetConnectiontId(_clientId.ToString());
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Got request from client,  [POS: " + _clientId.ToString() + "] [Connection ID: " + _connectionId + "]" );
        
            // communication with DigitaReceipt server
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("communication with DR,    [POS: " + _clientId.ToString() + "] [Connection ID: " + _connectionId + "]" );
            Thread.Sleep(2000);

            // send feedback to client
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("send client response,     [POS: " + _clientId.ToString() + "] [Connection ID: " + _connectionId + "]" );

            var response = new DRclientResponse() { 
                _clientId= _clientId.ToString(), 
                _QRcode= _clientId.ToString() + "|" + _connectionId
            }; 
            
            await _messageHub.Clients.Client(_connectionId).SendAsync("ReceiveMessage", JsonSerializer.Serialize<DRclientResponse>(response));

            return new DRfiscalResponse() {_errorCode=null, _UID= response._QRcode} ;

         }
    
    
    
}