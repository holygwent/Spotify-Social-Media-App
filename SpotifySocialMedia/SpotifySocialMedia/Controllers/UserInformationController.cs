using Microsoft.AspNetCore.Mvc;

namespace SpotifySocialMedia.Controllers
{

    public class UserInformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
