using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyCloneAPI.API.Models.Spotify;

namespace SpotifyCloneAPI.API.Services;

public interface ISpotifyService
{
    SpotifyClient GetSpotifyClient(string access_token);
    Task<PublicUser> GetUserProfile(string userId, string access_token);
    Task<CurrentUserProfileResponse> GetCurrentUserProfile(string access_token);
}
