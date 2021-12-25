using System.Text.Json;
using System;

namespace WebApi.Helpers
{

    public partial class AppSettings
    {
        public SpotifySettings SpotifySettings { get; set; }
    }
    public partial class SpotifySettings
    {
        public string BaseUrl { get; set; }
        public string LoginUrl { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Scopes { get; set; }
        public string JWTSecret { get; set; }
        public int RefreshTokenTTL { get; set; }
        public string RedirectURI { get; set; }
    }

    public partial class ApplicationWebSettings
    {
        public string BaseUrl { get; set; }
    }

    public partial class ApplicationAPISettings
    {
        public string BaseUrl { get; set; }
    }
}