using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DRproxy.Classes;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using anybill.POS.Client.Exceptions;
using anybill.POS.Client.Factories;
using anybill.POS.Client.Models.Bill;
using anybill.POS.Client.Models.Bill.CashRegister;
using anybill.POS.Client.Models.Bill.Data;
using anybill.POS.Client.Models.Bill.Data.VatAmount;
using anybill.POS.Client.Models.Bill.Head;
using anybill.POS.Client.Models.Bill.Misc;
using anybill.POS.Client.Models.Bill.Misc.Extension;
using anybill.POS.Client.Models.Bill.Response;
using anybill.POS.Client.Models.Bill.Security;
using anybill.POS.Client.Models.Bill.Security.Extension;
using anybill.POS.Client.Options;


namespace DRproxy.Services;

public class TokenStorageService : ITokenStorageService
{
    public required string Url { get; set; } = "https://adanybill.b2clogin.com/ad.anybill.de/oauth2/v2.0/token";
    public required string User { get; set; } = "diebold-nixdorf-stg";
    public required string Password { get; set; } = "kYIm$$~\"_Gk94,";
    public required string Client_id { get; set; } = "e2db9b99-866f-41b8-b148-bf501dc11d4a";
    public required string Query_param { get; set; } = "b2c_1_ropc_vendor";

    private string? _Token;
    private DateTime? _TokenExpiryDate;
    private string? _RefreshToken;
    private DateTime? _RefreshTokenExpiryDate;

    private readonly ILogger _logger;

    public TokenStorageService(ILogger<TokenStorageService> logger)
    {
        _Token = null;
        _TokenExpiryDate = null;
        _RefreshToken = null;
        _RefreshTokenExpiryDate = null;
        _logger = logger;
    }

    public async Task<String?> Get()
    {

        if (_Token is null)
        {
            _logger.LogInformation($"Getting new token");
            if (!await GetTokenFromAPIAsync())
            {
                _logger.LogError($"");
            }
        }
        else
        {
            if (DateTime.Now > _TokenExpiryDate!.Value.AddSeconds(-10))
            {
                if ((_RefreshTokenExpiryDate is null) || ((DateTime.Now < _RefreshTokenExpiryDate!.Value.AddSeconds(-10))))
                {
                    _logger.LogInformation($"Token Expired getting new token by using RefreshToken");
                    await RefreshTokenFromAPIAsync();
                }
                else
                {
                    _logger.LogInformation($"Refresh Token Expired getting new token by using RefreshToken");
                    await GetTokenFromAPIAsync();
                }
            }
        }
        return _Token;
    }

    private string AddQueryParametersToURL(string sUrl, IDictionary<string, string> Params)
    {
        var builder = new UriBuilder(sUrl);
        var query = HttpUtility.ParseQueryString(builder.Query);
        foreach (KeyValuePair<string, string> param in Params)
        {
            query[param.Key] = param.Value;
        }
        builder.Query = query.ToString();
        return builder.ToString();
    }

    private void ProcessResponse(AnybillTokenResponse response)
    {
        _Token = response.AccessToken; 
        _RefreshToken = response.RefreshToken;
        _TokenExpiryDate = DateTime.Now.AddSeconds(Convert.ToInt32(response.ExpiresIn));
        if (response.RefreshTokenExpiresIn is null)
            _RefreshTokenExpiryDate = null;
        else
            _RefreshTokenExpiryDate = DateTime.Now.AddSeconds(Convert.ToInt32(response.RefreshTokenExpiresIn));
    }

    private async Task<Boolean> GetTokenFromAPIAsync()
    {
        using HttpClient client = new();
        // create request body      
        var dict = new Dictionary<string, string>{
                                                     {"grant_type","password"}
                                                    ,{"username", User}
                                                    ,{"password", Password}
                                                    ,{"client_id",Client_id}
                                                    ,{"scope","https://ad.anybill.de/vendor/store offline_access"}
                                                    ,{"response_type","token"}
                                                };
        // create query param
        var url = AddQueryParametersToURL(Url, new Dictionary<string, string>() { { "p", Query_param } });
        // create Http request message
        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
        var response = await client.SendAsync(request);
        var contents = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation($"Token Response Ok : {response.StatusCode}");      
            if (contents is not null) {
                var responseObj = JsonSerializer.Deserialize<AnybillTokenResponse>(contents!);
                ProcessResponse(responseObj!);
                return true;
            }
            _logger.LogInformation($"Contnet == NULL [Function Exit]");            
        }

        _logger.LogError($"Token Response Invalid : {response.StatusCode}");
        _Token = null;
        _TokenExpiryDate = null;
        _RefreshToken = null;
        _RefreshTokenExpiryDate = null;
        return false;
    }

    private async Task<Boolean> RefreshTokenFromAPIAsync()
    {
        using HttpClient client = new();
        // create request body  
        var dict = new Dictionary<string, string>{
                                                     {"grant_type","password"}
                                                    ,{"refresh_token",_RefreshToken!}
                                                    ,{"client_id", Client_id}
                                                    ,{"scope","https://ad.anybill.de/vendor/store offline_access"}
                                                    ,{"response_type","token"}
                                                };
                                    
        // create query param
        var url = AddQueryParametersToURL(Url, new Dictionary<string, string>() { { "p", Query_param } });
        // create Http request message
        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
        var response = await client.SendAsync(request);
        var contents = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
             _logger.LogInformation($"TokenRefresh Response Ok : {response.StatusCode}");      
            if (contents is not null) {
                var responseObj = JsonSerializer.Deserialize<AnybillTokenResponse>(contents!);
                ProcessResponse(responseObj!);
                return true;
            }
            _logger.LogInformation($"Contnet == NULL [Function Exit]");            
        }

        _logger.LogError($"TokenRefresh Response Invalid : {response.StatusCode}");
        _Token = null;
        _TokenExpiryDate = null;
        _RefreshToken = null;
        _RefreshTokenExpiryDate = null;
        return false; 
    }


}
