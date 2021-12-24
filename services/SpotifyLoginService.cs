using Microsoft.Extensions.Options;
using SpotifyAPI.Web;
using WebApi.Models.Spotify;

namespace WebApi.Services
{
    public class SpotifyLoginService : ISpotifyLoginService
    {
        private readonly SpotifySettings _spotifySettings;
        private readonly ApplicationClientSettings _appClientSettings;

        public SpotifyLoginService(IOptions<SpotifySettings> spotifySettings
            , IOptions<ApplicationClientSettings> appClientSettings)
        {
            _spotifySettings = spotifySettings.Value;
            _appClientSettings = appClientSettings.Value;
        }

        public string GetLoginUrl()
        {
            var loginRequest = new LoginRequest(
                new Uri(_appClientSettings.BaseUrl),
                _spotifySettings.ClientID,
                LoginRequest.ResponseType.Code
            )
            {
                Scope = _spotifySettings.Scopes.Split(","),
            };

            var uri = loginRequest.ToUri();
            return uri.AbsoluteUri;
        }
    }
}