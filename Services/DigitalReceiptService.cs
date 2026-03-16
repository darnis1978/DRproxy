using System.Text.Json.Nodes;
using Microsoft.AspNetCore.SignalR;
using DRproxy.Hubs;
using anybill.POS.Client.Factories;
using anybill.POS.Client.Options;
using anybill.POS.Client;
using Microsoft.AspNetCore.Mvc;
using anybill.POS.Client.Exceptions;
using anybill.POS.Client.Models.Bill;
using anybill.POS.Client.Models.Bill.Response;

namespace DRproxy.Services;

public abstract class DigitalReceiptService: IDigitalReceiptService
{    

    private  AnybillClientFactory? _clientFactory;
    private  IAnybillClient? _anybillClient;
    private bool _auth = false;

    public IAnybillClient AnybillClient => _anybillClient!;
    public required string User { get; set; } = "";
    public required string Password { get; set; } = "";
    public required string Client_id { get; set; } = "";
    public required string MerchantId { get; set; } = "";



    public void CreateService()
    {
        _clientFactory = new AnybillClientFactory();
        _anybillClient = _clientFactory.Create(x => x.WithUsername(User).WithPassword(Password).WithClientId(Client_id), AnybillEnvironment.Staging);     
    }

     public virtual async Task CreateToken()
     {       
        try {
            // create authentication token
            await AnybillClient.Authentication.EnsureAsync();  
            _auth = true;
        } catch (AuthenticationException)
        {
            _auth = false;
            throw;
        }
    }


    // public virtual async Task<bool> SendBill(AddBill bill, AddBillOptions billOptions)
    // {
        
    //     try {
    //         var billResponse = await _anybillClient!.Bill.CreateAsync(bill, billOptions);
    //         switch (billResponse)
    //         {
    //             case IExternalIdResponse externalIdResponse:
    //                 // _logger.LogInformation("IsAssigned: " + externalIdResponse.IsAssigned);
    //                 break;
    //             case ILoyaltyCardResponse loyaltyCardResponse:
    //                 // _logger.LogInformation("IsAssigned: " + loyaltyCardResponse.IsAssigned);
    //                 break;
    //             case IMatchedBillResponse matchedBillResponse:
    //                 // _logger.LogInformation("IsAssigned: " + matchedBillResponse.IsAssigned);
    //                 break;
    //             case IUrlBillResponse urlBillResponse:
    //                 // _logger.LogInformation("GetMy website: " + urlBillResponse.Url);
    //                 break;
    //             case IUserIdResponse userIdResponse:
    //                 // _logger.LogInformation("IsAssigned: " + userIdResponse.IsAssigned);
    //                 break;
    //         }
    //     } catch (AuthenticationException authenticationException)
    //     {
    //         // Authentication failed because username, password or clientId is wrong.
    //         _logger.LogError(authenticationException.ErrorDescription);
    //         _auth = false;
    //         return new OkObjectResult(null);
    //     }
    //     catch (BadRequestException badRequestException)
    //     {
    //         // Request failed. e.g. validation error.
    //         _logger.LogError(badRequestException.Message);
    //         return new OkObjectResult(null);
    //     }
    //     catch (ForbiddenException forbiddenException)
    //     {
    //         // Request failed because user is not allowed to use the called endpoint.
    //         _logger.LogError(forbiddenException.Message);
    //         return new OkObjectResult(null);
    //     }
    //     catch (NotFoundException notFoundException)
    //     {
    //         // Request failed because called endpoint does not exists.
    //         _logger.LogError(notFoundException.Message);
    //         return new OkObjectResult(null);
    //     }
    //     catch (UnauthorizedException unauthorizedException)
    //     {
    //         // Request failed because user is not authorized.
    //         _logger.LogError(unauthorizedException.Message);
    //         return new OkObjectResult(null);
    //     }
    //     catch (UnhandledException unhandledException)
    //     {
    //         // Request failed for some reason.
    //         _logger.LogError(unhandledException.Message);
    //         return new OkObjectResult(null);
    //     }
    //     catch(Exception e) {
    //         _logger.LogError(e.Message);
    //         return new OkObjectRe

    //     return true;
    // }

    public abstract Task<ActionResult>ProcessTransaction(JsonObject json);

}