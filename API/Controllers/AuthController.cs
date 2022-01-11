using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpotifyCloneAPI.API.Models.Spotify;
using SpotifyCloneAPI.API.Helpers;
using SpotifyCloneAPI.API.Services;

namespace SpotifyCloneAPI.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISpotifyAuthService _spotifyAuthService;
    private readonly ISpotifyService _spotifyService;
    private readonly ILogger<AuthController> _logger;

    private readonly ApplicationWebSettings _appWebSettings;
    public AuthController(ISpotifyAuthService spotifyAuthService,
                          ISpotifyService spotifyService,
                          IOptions<ApplicationWebSettings> appWebSettings,
                          ILogger<AuthController> logger)
    {
        _spotifyAuthService = spotifyAuthService;
        _appWebSettings = appWebSettings.Value;
        _spotifyService = spotifyService;
        _logger = logger;
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

        if (string.IsNullOrEmpty(refreshToken))
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
    public IActionResult RevokeToken()
    {
        TokenUtils.RemoveTokenCookie(Response);
        return Ok();
    }
}
