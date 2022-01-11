using Microsoft.AspNetCore.Mvc;
using SpotifyCloneAPI.API.Services;
using SpotifyCloneAPI.API.Helpers;
using SpotifyAPI.Web;

namespace SpotifyCloneAPI.API.Controllers;

[Route("[controller]")]
[ApiController]
public class SpotifyController : ControllerBase
{
    private readonly ISpotifyService _spotifyService;
    private readonly ILogger<SpotifyController> _logger;

    public SpotifyController(ISpotifyService spotifyService, ILogger<SpotifyController> logger)
    {
        _spotifyService = spotifyService;
        _logger = logger;
    }

    [HttpGet("userprofile/{userId}")]
    public async Task<IActionResult> GetUserProfile(string userId)
    {
        PublicUser userProfile = await _spotifyService.GetUserProfile(userId, TokenUtils.GetAccessToken(Request));
        return Ok(userProfile);
    }
}
