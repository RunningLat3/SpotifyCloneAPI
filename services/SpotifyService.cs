using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using WebApi.Helpers;
using WebApi.Models.Spotify;

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
            if (String.IsNullOrEmpty(access_token))
                throw new AppException("Missing access token");

            var userProfile = await GetSpotifyClient(access_token).UserProfile.Get(userId);
            return userProfile;
        }

        public async Task<CurrentUserResponse> GetCurrentUserProfile(string access_token)
        {
            if (String.IsNullOrEmpty(access_token))
                throw new AppException("Missing access token");

            var userProfile = await GetSpotifyClient(access_token).UserProfile.Current();
            if (userProfile is null) 
                return new CurrentUserResponse();
                
            return new CurrentUserResponse() {
                Country = userProfile.Country,
                DisplayName = userProfile.DisplayName,
                Email = userProfile.Email,
                Followers = userProfile.Followers,
                Href = userProfile.Href,
                Id = userProfile.Id,
                Product = userProfile.Product,
                Images = userProfile.Images,
                ExternalUrls = userProfile.ExternalUrls,
                Uri = userProfile.Uri,
                Type = userProfile.Type
            };

        }
    }
}