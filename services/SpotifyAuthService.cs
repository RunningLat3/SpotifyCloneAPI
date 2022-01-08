using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SpotifyAPI.Web;
using WebApi.Helpers;
using WebApi.Models.Spotify;

namespace WebApi.Services
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly SpotifySettings _spotifySettings;

        private readonly ISpotifyService _spotifyService;

        public SpotifyAuthService(IOptions<SpotifySettings> spotifySettings, ISpotifyService spotifyService)
        {
            _spotifySettings = spotifySettings.Value;
            _spotifyService = spotifyService;
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
                State = TokenUtils.RandomString(16, true)
            };

            var uri = loginRequest.ToUri();
            return uri.AbsoluteUri;
        }

        public async Task<TokenResponse> GetToken(AuthorizationCodeRequest request)
        {
            if (!string.IsNullOrEmpty(request.Error))
                throw new Exception(request.Error);

            if (String.IsNullOrEmpty(request.State))
                throw new Exception("state_mismatch");

            string code = request.Code ?? "";
            var tokenResponse = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(_spotifySettings.ClientID, _spotifySettings.ClientSecret, code, new Uri(_spotifySettings.RedirectURI))
            );

            return new TokenResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                CreatedAt = tokenResponse.CreatedAt,
                ExpiresIn = tokenResponse.ExpiresIn,
                Scope = tokenResponse.Scope,
                TokenType = tokenResponse.TokenType
            };
        }

        public async Task<TokenResponse> GetRefreshToken(RefreshTokenRequest request)
        {

            if (String.IsNullOrEmpty(request.RefreshToken))
                throw new Exception("Missing refresh token");

            string refreshToken = request.RefreshToken ?? "";
            var tokenResponse = await new OAuthClient().RequestToken(
                new AuthorizationCodeRefreshRequest(_spotifySettings.ClientID, _spotifySettings.ClientSecret, refreshToken)
            );

            return new TokenResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                CreatedAt = tokenResponse.CreatedAt,
                ExpiresIn = tokenResponse.ExpiresIn,
                Scope = tokenResponse.Scope,
                TokenType = tokenResponse.TokenType
            }; ;
        }


        public async Task<CurrentUserProfileResponse> GetTokenCurrentUserProfile(TokenRequest request)
        {
            var tokenResponse = new TokenResponse();
            switch (request.TokenRequestType)
            {
                case TokenRequestType.Access:
                    tokenResponse = await this.GetToken(new AuthorizationCodeRequest()
                    {
                        Code = request.Code,
                        Error = request.Error,
                        State = request.State
                    });
                    break;
                case TokenRequestType.Refresh:
                    tokenResponse = await this.GetRefreshToken(new RefreshTokenRequest()
                    {
                        RefreshToken = request.RefreshToken,
                    });
                    break;
            }

            var currentUserProfile = await _spotifyService.GetCurrentUserProfile(tokenResponse.AccessToken);
            string refreshToken = (tokenResponse.RefreshToken ?? request.RefreshToken ?? "");
            currentUserProfile.AccessToken = tokenResponse.AccessToken;
            currentUserProfile.ExpiresIn = tokenResponse.ExpiresIn;
            currentUserProfile.Scope = tokenResponse.Scope;
            currentUserProfile.TokenType = tokenResponse.TokenType;
            currentUserProfile.RefreshToken = refreshToken;

            return currentUserProfile;
        }
    }
}