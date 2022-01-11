using System.Text.Json.Serialization;

namespace SpotifyCloneAPI.API.Models.Spotify;

public class RefreshTokenRequest
{
    public string? RefreshToken { get; set; }
    public string GrantType { get; set; } = "refresh_token";
}
