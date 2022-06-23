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
           var songs =  await _searchService.SearchSong(search);
            return View(songs);
        }
    }
}
