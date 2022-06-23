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

        public AuthorizationController(IAuthorizeService authorizeService, IConfiguration configurationAppSettingJSON
            ,IDatabaseAuthorizationCodeService databaseAuthorizationCodeService)
        {
            _authorizeService = authorizeService;
            _configurationAppSettingJSON = configurationAppSettingJSON;
            _databaseAuthorizationCodeService = databaseAuthorizationCodeService;
        }
        [HttpGet("Home")]
        public IActionResult Home()
        {
            return Ok("git");
        }


        [HttpGet("AuthorizationCode")]
        public IActionResult AuthorizationCode()
        {
            string url = _authorizeService.GetLinkToAuthorize();
            return Redirect(url);
        }

        [HttpGet("CallBack")]
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
