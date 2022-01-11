using System.Threading.Tasks;
using SpotifyCloneAPI.API.Models.Spotify;

namespace SpotifyCloneAPI.API.Services;

public interface ISpotifyAuthService
{
    string GetLoginUrl();
    Task<TokenResponse> GetToken(AuthorizationCodeRequest request);
    Task<TokenResponse> GetRefreshToken(RefreshTokenRequest request);
    Task<CurrentUserProfileResponse> GetTokenCurrentUserProfile(TokenRequest request);
}
