using System.Text.Json.Serialization;

namespace WebApi.Models.Spotify {
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
        public string GrantType { get; set; } = "refresh_token";
    }
}