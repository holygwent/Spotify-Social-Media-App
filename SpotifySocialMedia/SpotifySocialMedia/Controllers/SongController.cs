using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using System.Diagnostics;

namespace SpotifySocialMedia.Controllers
{
    [Route("Song")]
    public class SongController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly ISongRepository _songRepository;

        public SongController(ISearchService searchService,ISongRepository songRepository)
        {
            _searchService = searchService;
           _songRepository = songRepository;
        }
        [Route("{id}")]
        public IActionResult Details([FromRoute]string id)
        {
            var song = _songRepository.GetSong(id).Result;
            if (song is null)
            {
                _songRepository.CreateSong(id).Wait();
                song = _songRepository.GetSong(id).Result;
            }
           
            return View(song);
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
