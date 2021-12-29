using System.Text.Json.Serialization;

namespace WebApi.Models.Spotify
{
    public class TokenRequest
    {
        public string? Code { get; set; }
        public string? Error { get; set; }
        public string? State { get; set; }
        public string? RefreshToken { get; set; }
        public string GrantType { get; set; } = "refresh_token";
        public TokenRequestType? TokenRequestType { get; set; } = null;
    }

    public enum TokenRequestType
    {
        Refresh,
        Access
    }
}