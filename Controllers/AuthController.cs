using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Models.Spotify;
using WebApi.Services;

namespace SpotifyCloneAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISpotifyAuthService _spotifyAuthService;
        private readonly ISpotifyService _spotifyService;

        private readonly ApplicationWebSettings _appWebSettings;
        public AuthController(IOptions<ApplicationWebSettings> appWebSettings, ISpotifyAuthService spotifyAuthService, ISpotifyService spotifyService)
        {
            _spotifyAuthService = spotifyAuthService;
            _appWebSettings = appWebSettings.Value;
            _spotifyService = spotifyService;
        }

        [HttpGet("login-url")]
        public IActionResult LoginUrl()
        {
            return Ok(_spotifyAuthService.GetLoginUrl());
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(TokenRequest request)
        {
            var currentTokenUserProfile = await _spotifyAuthService.GetTokenCurrentUserProfile(request);
            TokenUtils.SetTokenCookie(Response, currentTokenUserProfile.RefreshToken, currentTokenUserProfile.ExpiresIn);
            return Ok(currentTokenUserProfile);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> GetRefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if(string.IsNullOrEmpty(refreshToken))
                return NoContent();

            var currentTokenUserProfile = await _spotifyAuthService.GetTokenCurrentUserProfile(new TokenRequest()
            {
                RefreshToken = refreshToken,
                TokenRequestType = TokenRequestType.Refresh
            });

            TokenUtils.SetTokenCookie(Response, currentTokenUserProfile.RefreshToken, currentTokenUserProfile.ExpiresIn);
            return Ok(currentTokenUserProfile);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken() {
            TokenUtils.RemoveTokenCookie(Response);
            return Ok();
        }
    }
}