using System.Threading.Tasks;
using SpotifyAPI.Web;
using WebApi.Models.Spotify;

namespace WebApi.Services {
    public interface ISpotifyAuthService {
        string GetLoginUrl();
        Task<TokenResponse> GetToken(AuthorizationCodeRequest request);
        Task<TokenResponse> GetRefreshToken(RefreshTokenRequest request);
        Task<CurrentUserProfileResponse> GetTokenCurrentUserProfile(TokenRequest request);
    }
}