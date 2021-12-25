using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SpotifyAPI.Web;
using WebApi.Models.Spotify;

namespace WebApi.Services
{
    public class SpotifyLoginService : ISpotifyLoginService
    {
        private readonly SpotifySettings _spotifySettings;
        private readonly ApplicationWebSettings _appWebSettings;

        public SpotifyLoginService(IOptions<SpotifySettings> spotifySettings
            , IOptions<ApplicationWebSettings> appWebSettings)
        {
            _spotifySettings = spotifySettings.Value;
            _appWebSettings = appWebSettings.Value;
        }

        public string GetLoginUrl()
        {
            var loginRequest = new LoginRequest(
                new Uri(_spotifySettings.RedirectURI),
                _spotifySettings.ClientID,
                LoginRequest.ResponseType.Code
            )
            {
                Scope = _spotifySettings.Scopes.Split(","),
            };

            var uri = loginRequest.ToUri();
            return uri.AbsoluteUri;
        }

        public async Task<AuthorizationCodeTokenResponse?> Authenticate(RequestAuthorizationResponse response)
        {
            if (!String.IsNullOrEmpty(response.State))
                return null;

            var tokenResponse = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(_spotifySettings.ClientID, _spotifySettings.ClientSecret, response.Code, new Uri(_spotifySettings.RedirectURI))
            );
            
            return tokenResponse;
        }
    }
}