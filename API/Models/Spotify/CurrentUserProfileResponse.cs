using System.Collections.Generic;
using System.Text.Json.Serialization;
using SpotifyAPI.Web;

namespace SpotifyCloneAPI.API.Models.Spotify;

public class CurrentUserProfileResponse
{
    public string Country { get; set; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    public string Birthdate { get; set; }
    public string Email { get; set; }
    [JsonPropertyName("external_urls")]
    public Dictionary<string, string> ExternalUrls { get; set; }
    public Followers Followers { get; set; }
    public string Href { get; set; }
    public string Id { get; set; }
    public List<Image> Images { get; set; }
    public string Product { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }

    public string Scope { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
}
