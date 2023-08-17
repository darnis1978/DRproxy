using System.Text.Json.Serialization;

namespace DRproxy.Classes;

public class AnybillTokenResponse
{
    [JsonPropertyName("access_token")]
    public  string? AccessToken { get; set; }
     [JsonPropertyName("token_type")]
    public  string? TokenType { get; set; }
     [JsonPropertyName("expires_in")]
    public  string? ExpiresIn { get; set; }
      [JsonPropertyName("refresh_token")]
    public  string? RefreshToken { get; set; }
      [JsonPropertyName("refresh_token_expires_in")]
    public string? RefreshTokenExpiresIn { get; set; }
}
