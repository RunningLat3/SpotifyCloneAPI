using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Spotify;
namespace SpotifyCloneAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly ISpotifyLoginService _spotifyLoginService;
        public AuthController(ISpotifyLoginService spotifyLoginService)
        {
            _spotifyLoginService = spotifyLoginService;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login() {
            return Redirect(_spotifyLoginService.GetLoginUrl());
        }

        [AllowAnonymous]
        [HttpPost("callback")]
        public async Task<IActionResult> Authenticate([FromQuery] RequestAuthorizationResponse response) {
            var tokenResponse = await _spotifyLoginService.Authenticate(response);
            if (tokenResponse == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "state_mismatch"});

            return Ok(tokenResponse);
        }
    }
}