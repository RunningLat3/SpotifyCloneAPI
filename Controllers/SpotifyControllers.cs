using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace SpotifyCloneAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase {
        private readonly ISpotifyService _spotifyService;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [AllowAnonymous]
        [HttpGet("userprofile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId) {
            PublicUser userProfile = await _spotifyService.GetUserProfile(userId, TokenUtils.GetAccessToken(Request));
            return Ok(userProfile);
        }
    }
}