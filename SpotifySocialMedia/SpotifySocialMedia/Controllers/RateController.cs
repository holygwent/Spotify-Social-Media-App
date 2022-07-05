using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifySocialMedia.Services.Repositories.Interfaces;

namespace SpotifySocialMedia.Controllers
{
    public class RateController : Controller
    {
        private readonly IRateRepository _rateRepository;

        public RateController(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }
       
        [Authorize]
        public IActionResult AddRate(string songId, string user, int starValue)
        {
            _rateRepository.Add(songId,user,starValue).Wait();
            return Ok(_rateRepository.GetAverageRate().Result);
        }
    }
}
