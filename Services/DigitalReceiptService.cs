using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using DRproxy.Hubs;
using DRproxy.Services;
using FiscalModel;

using System.Text.Json.Nodes;
using System.Linq;

namespace DRproxy.Services;

public class DigitalReceiptService: IDigitalReceiptService
{    
    
    private readonly IMemeoryStorageService _connectionsPool; 
    private readonly IHubContext <MessageHub> _messageHub;
    private readonly ITokenStorageService _tokenStorage;


    public DigitalReceiptService(
                                IHubContext < MessageHub > messageHub, 
                                IMemeoryStorageService connectionPool,
                                ITokenStorageService tokenStorage
                                ){

        _connectionsPool = connectionPool;
        _messageHub = messageHub;
        _tokenStorage = tokenStorage;
    }

    public async Task<DRfiscalResponse> ProcessTransaction(JsonObject json) {


        FiscalDTO fiscalDTO;
        string clientId;
        string connectionId; 
        string? token;
 

        try {
            
            // parse request body to FiscalDTO model
            fiscalDTO = FiscalDTO.FromJson(json.ToJsonString());

            // get client id (POS number) from fiscalDTO     
            clientId = fiscalDTO.Body.OfType<Body>().First(item => item.FiscalFooter !=null).FiscalFooter.CashNumber;

            // get connection id for this client id from connections pool
            connectionId = _connectionsPool.GetConnectiontId(clientId);
            connectionId = "";
            if (connectionId.Length == 0)
                throw new Exception( message: $"there is no connectionId for clientId: {clientId}" );

            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Got request from client,  [POS: " + clientId + "] [Connection ID: " + connectionId + "]" );

            // get token
            token = await _tokenStorage.Get();
            if ( token == null )
                 throw new Exception( message: "token was not created" );


    



            // communication with DigitaReceipt server
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("communication with DR,    [POS: " + clientId + "] [Connection ID: " + connectionId + "]" );
                
            
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("token is stored" );
            
            // send receipt to reciever 
            Thread.Sleep(2000);

            // send feedback to client
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("send client response,     [POS: " + clientId.ToString() + "] [Connection ID: " + connectionId + "]" );



            var response = new DRclientResponse() { 
                ClientId= clientId.ToString(), 
                QRcode= clientId.ToString() + "|" + connectionId
            }; 
            
            await _messageHub.Clients.Client(connectionId).SendAsync("ReceiveMessage", JsonSerializer.Serialize<DRclientResponse>(response));

            return new DRfiscalResponse() {ErrorCode=null, UID= response.QRcode} ;
        
        } catch(Exception e) {
            Console.WriteLine(e.Message);
            return new DRfiscalResponse() { ErrorCode = 1, UID= null } ;
        }

    }           
}  



            // foreach(var body in fiscalDTO.Body) {
            //     if (body.FiscalFooter != null)
            //         clientId = body.FiscalFooter.CashNumber;
            // }