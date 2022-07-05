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
            return Ok(_rateRepository.GetAverageRate(songId).Result);
        }
        
        public IActionResult GetAverageRate(string songId)
        {
           var data= _rateRepository.GetAverageRate(songId).Result;
            return Ok(data);
        }
    }
}
