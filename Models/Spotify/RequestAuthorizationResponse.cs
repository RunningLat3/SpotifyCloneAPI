using System.Text.Json;

namespace WebApi.Models.Spotify {
    public class RequestAuthorizationResponse
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }

    }
}