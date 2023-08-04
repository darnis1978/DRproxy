using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using DRproxy.Hubs;
using DRproxy.Services;

namespace DRproxy.Services;

public class DigitalReceiptService: IDigitalReceiptService
{    
    
    private readonly IMemeoryStorageService _connectionsPool; 
    private IHubContext <MessageHub> _messageHub;

    private ITokenStorageService _tokenStorage;


        public DigitalReceiptService(
                                    IHubContext < MessageHub > messageHub, 
                                    IMemeoryStorageService connectionPool,
                                    ITokenStorageService tokenStorage
                                    ){

            _connectionsPool = connectionPool;
            _messageHub = messageHub;
            _tokenStorage = tokenStorage;
        }

        public async Task<DRfiscalResponse> processTransaction(JsonDocument txt){
    
    
            Dictionary<string, object>? payload = JsonSerializer.Deserialize<Dictionary<string, object>>(txt);
            if (payload == null) {       
                return new DRfiscalResponse() { _errorCode= 1, _UID= null } ;
            }
        
            // get client id from TX
            Object? _clientId; 
            payload!.TryGetValue("PosNmbr" , out _clientId);

            // get connection id from connections pool
            string _connectionId = _connectionsPool.GetConnectiontId(_clientId!.ToString()!);
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Got request from client,  [POS: " + _clientId.ToString() + "] [Connection ID: " + _connectionId + "]" );
        
            // communication with DigitaReceipt server
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("communication with DR,    [POS: " + _clientId.ToString() + "] [Connection ID: " + _connectionId + "]" );
             
            // get token
            string? token= await _tokenStorage.Get();
         
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("token is stored" );
            
            // send receipt to reciever 
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