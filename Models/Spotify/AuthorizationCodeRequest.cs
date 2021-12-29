using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Models.Spotify {
    public class AuthorizationCodeRequest
    {
        public string? Code { get; set; }
        public string? Error { get; set; }
        public string? State { get; set; }

    }
}