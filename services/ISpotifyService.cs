using System.Threading.Tasks;
using SpotifyAPI.Web;
using WebApi.Models.Spotify;

namespace WebApi.Services
{
    public interface ISpotifyService
    {
        SpotifyClient GetSpotifyClient(string access_token);
        Task<PublicUser> GetUserProfile(string userId, string access_token);
        Task<CurrentUserResponse> GetCurrentUserProfile(string access_token);
    }
}