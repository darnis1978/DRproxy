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

namespace DRproxy.Services;

public class TokenStorageService : ITokenStorageService
{
    public required string Url {get; set;} = "https://vendor.stg.anybill.de/api/v3/store";
    public required string User {get; set;} = "diebold-nixdorf-stg";
    public required string Password {get;set;}= "kYIm$$~\"_Gk94,";
    public required string Client_id {get;set;} = "e2db9b99-866f-41b8-b148-bf501dc11d4a";
    private string? _Token;
    private readonly ILogger _logger;
    private DateTime? _TokenExpiryDate;
    private string? _RefreshToken {get;set;}
    private DateTime? _RefreshTokenExpiryDate;
    
    
    public TokenStorageService(ILogger<TokenStorageService> logger)
    {
        _Token=null;
        _TokenExpiryDate=null; 
        _RefreshToken=null;
        _RefreshTokenExpiryDate=null;
        _logger=logger;        
    }

    public async Task<String?> Get()
    {
        if (_Token is null)
        {
            _logger.LogInformation($"Getting new token");
            if( !await GetTokenFromAPIAsync ())
            {
                _logger.LogError($"");
            }
        }
        else
        {
            if (DateTime.Now > this._TokenExpiryDate!.Value.AddSeconds(-10))
            {
                if ((_RefreshTokenExpiryDate is null)  || ((DateTime.Now <_RefreshTokenExpiryDate!.Value.AddSeconds(-10))))
                {
                    _logger.LogInformation($"Token Expired getting new token by using RefreshToken");
                    await RefreshTokenFromAPIAsync(Url);
                }
                else 
                {
                    _logger.LogInformation($"Refresh Token Expired getting new token by using RefreshToken");
                    await GetTokenFromAPIAsync ();
                }
            }
        }
        return _Token;
    }

    private string  AddQueryParametersToURL(string sUrl,IDictionary<string,string> Params)
    {
        var builder=new UriBuilder(sUrl);
        var query = HttpUtility.ParseQueryString(builder.Query);
        foreach( KeyValuePair<string,string> param in Params )
        {
           query[param.Key] =param.Value;    
        }
        builder.Query = query.ToString();
        return builder.ToString();
    }

    
    private async Task<Boolean> GetTokenFromAPIAsync () 
    {
    using (HttpClient client = new HttpClient())
        {
            
            
            var dict=new Dictionary<string,string>{{"grant_type","password"}
                                                    ,{"username",User}
                                                    ,{"password",Password}
                                                    ,{"client_id",Client_id}
                                                    ,{"scope","https://ad.anybill.de/vendor/store offline_access"}
                                                    ,{"response_type","token"}
                                                };
            var _url= AddQueryParametersToURL (this.Url,new Dictionary<string,string>(){{"p","b2c_1_ropc_vendor"}});

            var request = new HttpRequestMessage(HttpMethod.Post, _url){Content=new FormUrlEncodedContent(dict)};
            
            
            // Nic nie zmienia - 
            // request.Content.Headers.ContentType= MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded"); 
            
            // Nic nie zmienia 
            // request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")); 
            
            // Nie Pozwala 
            // request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);

            var contents = await response.Content.ReadAsStringAsync();


            if (response.StatusCode==System.Net.HttpStatusCode.OK)
            {       
                _logger.LogInformation($"Token Response Ok : {response.StatusCode} , {contents}");
                var responseObj  = JsonSerializer.Deserialize<AnybillResponseTokenJSON>(contents);
                this._Token=responseObj.access_token;
                if (Int32.TryParse(responseObj.refresh_token_expires_in,out int outSeconds))
                {
                    this._TokenExpiryDate=DateTime.Now.AddSeconds(outSeconds); 
                }
                this._TokenExpiryDate=DateTime.Now.AddSeconds(Convert.ToInt32(responseObj.expires_in)); 
                this._RefreshToken=responseObj.refresh_token;
                if (Int32.TryParse(responseObj.refresh_token_expires_in,out outSeconds))
                {
                    this._RefreshTokenExpiryDate=DateTime.Now.AddSeconds(outSeconds); 
                }
                return true;
            }
            else
            {
                _logger.LogError($"Token Response Invalid : {response.StatusCode} , {contents}");
                _Token=null;
                _TokenExpiryDate=null; 
                _RefreshToken=null;
                _RefreshTokenExpiryDate=null;                                
                return false;
            }    
        }
    }

    private async Task<Boolean> RefreshTokenFromAPIAsync (string sUrl) 
    {
    using (HttpClient client = new HttpClient())
        {

            var dict=new Dictionary<string,string>{{"grant_type","refresh_token"}
                                                    ,{"refresh_token",_RefreshToken}
                                                    ,{"password",Password}
                                                    ,{"scope","https://ad.anybill.de/vendor/store offline_access"}
                                                    ,{"response_type","token"}
                                                };
            var _url= AddQueryParametersToURL (this.Url,new Dictionary<string,string>(){{"p","b2c_1_ropc_vendor"}});

            var request = new HttpRequestMessage(HttpMethod.Post, _url){Content=new FormUrlEncodedContent(dict)};
            
            
            // Nic nie zmienia - 
            // request.Content.Headers.ContentType= MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded"); 
            
            // Nic nie zmienia 
            // request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")); 
            
            // Nie Pozwala 
            // request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);

            var contents = await response.Content.ReadAsStringAsync();


            if (response.StatusCode==System.Net.HttpStatusCode.OK)
            {       
                _logger.LogInformation($"Token Response Ok : {response.StatusCode} , {contents}");
                var responseObj  = JsonSerializer.Deserialize<AnybillResponseTokenJSON>(contents);
                this._Token=responseObj.access_token;
                if (Int32.TryParse(responseObj.refresh_token_expires_in,out int outSeconds))
                {
                    this._TokenExpiryDate=DateTime.Now.AddSeconds(outSeconds); 
                }
                this._TokenExpiryDate=DateTime.Now.AddSeconds(Convert.ToInt32(responseObj.expires_in)); 
                this._RefreshToken=responseObj.refresh_token;
                if (Int32.TryParse(responseObj.refresh_token_expires_in,out outSeconds))
                {
                    this._RefreshTokenExpiryDate=DateTime.Now.AddSeconds(outSeconds); 
                }
                return true;
            }
            else
            {
                _logger.LogError($"Token Response Invalid : {response.StatusCode} , {contents}");
                _Token=null;
                _TokenExpiryDate=null; 
                _RefreshToken=null;
                _RefreshTokenExpiryDate=null;                                
                return false;
            }    
        }
    }


}
