using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services;
using System.Diagnostics;

namespace SpotifySocialMedia.Controllers
{
    [Route("Song")]
    public class SongController : Controller
    {
        private readonly ISearchService _searchService;

        public SongController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [Route("{id}")]
        public IActionResult Details([FromRoute]string id)
        {
            SongItem item = new SongItem();
            item.Id = id;
            return View(item);
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
