using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Services;

namespace SpotifySocialMedia.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        public async Task<IActionResult> Index([FromQuery]string search)
        {
            if (search ==""| search is null)
            {
                return RedirectToAction("Index", "Home");
            }
           var songs =  await _searchService.SearchSong(search);
            return View(songs);
        }

        public async Task<IActionResult> GetPreviousOrNextSongs([FromQuery] string url)
        {
            if (url == "" | url is null)
            {
                return RedirectToAction("Index", "Home");
            }
            var songs = await _searchService.GetNextOrPreviousSongs(url);
            return View(songs);
        }
    }
}
