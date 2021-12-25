using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace WebApi.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly SpotifyClientConfig _spotifyClientConfig;

        public SpotifyService(SpotifyClientConfig spotifyClientConfig)
        {
            _spotifyClientConfig = spotifyClientConfig;
        }
        public SpotifyClient GetSpotifyClient(string access_token)
        {
            return new SpotifyClient(_spotifyClientConfig
                                        .WithToken(access_token)
                                        .WithRetryHandler(new SimpleRetryHandler() { RetryAfter = TimeSpan.FromSeconds(60) }));
        }

        public async Task<PublicUser> GetUserProfile(string userId, string access_token)
        {
            var userProfile = await GetSpotifyClient(access_token).UserProfile.Get(userId);
            return userProfile;
        }
    }
}