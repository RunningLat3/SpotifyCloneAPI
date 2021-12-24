using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyCloneServers.Controllers
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
        public IActionResult Authenticate() {
            return Ok();
        }
    }
}