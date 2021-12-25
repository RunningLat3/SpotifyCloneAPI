using System.Threading.Tasks;
using SpotifyAPI.Web;
using WebApi.Models.Spotify;

namespace WebApi.Services {
    public interface ISpotifyLoginService {
        string GetLoginUrl();
        Task<AuthorizationCodeTokenResponse?> Authenticate(RequestAuthorizationResponse response);
    }
}