using Microsoft.AspNetCore.Mvc;

namespace SpotifySocialMedia.Areas.Admin.Controllers
{
    public class SpotifyTokenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
