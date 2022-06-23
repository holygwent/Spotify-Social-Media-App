using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Models;
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
        public AuthorizationController(IAuthorizeService authorizeService, IConfiguration configurationAppSettingJSON)
        {
            _authorizeService = authorizeService;
            _configurationAppSettingJSON = configurationAppSettingJSON;
        }
        [HttpGet("Home")]
        public IActionResult Home()
        {
            return Ok("jest git routing");
        }
        [HttpGet("GetAuthorizeCode")]
        public IActionResult GetAuthorizeCode()
        {
            string url = _authorizeService.GetLinkToAuthorize();
            return Redirect(url);
        }

        [HttpGet("CallBack")]
        public IActionResult CallBack(string code, string state)
        {
            if (state == _configurationAppSettingJSON["Spotify:state"])
            {
                return Ok(code);
            }
            else
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }



        }


    }
}
