using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.SpotifySettingsDatabase.Models;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;
using System.Diagnostics;

namespace SpotifySocialMedia.Areas.Admin.Controllers
{
    // [Authorize(Roles ="Admin")]
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly IConfiguration _configurationAppSettingJSON;
        private readonly IDatabaseAuthorizationCodeService _databaseAuthorizationCodeService;
        private readonly ISpotifyTokenService _spotifyTokenService;
        private readonly IDatabaseSpotifyTokenService _databaseSpotifyTokenService;

        public AuthorizationController(IAuthorizeService authorizeService, IConfiguration configurationAppSettingJSON
            ,IDatabaseAuthorizationCodeService databaseAuthorizationCodeService,
            ISpotifyTokenService spotifyTokenService,
            IDatabaseSpotifyTokenService databaseSpotifyTokenService)
        {
            _authorizeService = authorizeService;
            _configurationAppSettingJSON = configurationAppSettingJSON;
            _databaseAuthorizationCodeService = databaseAuthorizationCodeService;
            _spotifyTokenService = spotifyTokenService;
            _databaseSpotifyTokenService = databaseSpotifyTokenService;
        }
        [Route("Home")]
        public IActionResult Home()
        {
            return Ok("git");
        }

        [Route("NewToken")]
        public  IActionResult NewToken()
        {
            _spotifyTokenService.SaveNewTokenToDatabase(_databaseAuthorizationCodeService.GetCode(), _configurationAppSettingJSON["Spotify:ClientId"], _configurationAppSettingJSON["Spotify:ClientSecret"]).Wait();
            return RedirectToAction("Home", "Authorization");
        }

        [Route("RefreshToken")]
        public IActionResult RefreshToken()
        {
            _spotifyTokenService.RefreshToken(_databaseSpotifyTokenService.GetToken().refresh_token, _configurationAppSettingJSON["Spotify:ClientId"], _configurationAppSettingJSON["Spotify:ClientSecret"]).Wait();
            return RedirectToAction("Home", "Authorization");
        }

        [Route("AuthorizationCode")]
        public IActionResult AuthorizationCode()
        {
            string url = _authorizeService.GetLinkToAuthorize();
            return Redirect(url);
        }

        [Route("CallBack")]
        public IActionResult CallBack(string code, string state)
        {
            if (state == _configurationAppSettingJSON["Spotify:state"])
            {
                _databaseAuthorizationCodeService.DropCode();
                _databaseAuthorizationCodeService.AddCode(new AuthorizationCode { Code = code  });
        
                return RedirectToAction("Home", "Authorization");
            }
            else
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }


    }
}
