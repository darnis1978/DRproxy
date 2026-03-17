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
        } catch (AuthenticationException)
        {
            throw;
        }
    }

    public abstract Task<ActionResult>ProcessTransaction(JsonObject json);

}