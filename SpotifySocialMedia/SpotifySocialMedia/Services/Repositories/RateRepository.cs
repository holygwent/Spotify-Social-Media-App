using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services.Repositories.Interfaces;

namespace SpotifySocialMedia.Services.Repositories
{
    public class RateRepository:IRateRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public RateRepository(ApplicationDbContext dbContext,UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task Add(string songId,string userEmail,int rate)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
         
            if (user is not null)
            {
                bool RatedInPast = _dbContext.Rates.Any(x => x.UserId == user.Id);
                if (RatedInPast)
                {
                   var pastRate = _dbContext.Rates.SingleOrDefault(x=>x.UserId == user.Id & x.SongId == songId);
                    if(pastRate != null)
                     _dbContext.Rates.Remove(pastRate);
                    await _dbContext.Rates.AddAsync(new Rate { UserId = user.Id, SongId = songId, Value = rate });
                }
                else
                {
                    await _dbContext.Rates.AddAsync(new Rate { UserId = user.Id, SongId = songId, Value = rate });
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AverageRate> GetAverageRate(string songId)
        {
            double averageValue=0;
            int numberOfEvaluators;
            numberOfEvaluators = _dbContext.Rates.Where(x => x.SongId == songId).Count();
            foreach (var rate in _dbContext.Rates.Where(x=>x.SongId==songId))
            {
                averageValue += (double)rate.Value;
            }

            if (numberOfEvaluators !=0)
            {
                averageValue =Math.Round( averageValue / numberOfEvaluators,2);
            }
            else
            {
                averageValue = 0;
            }
         

            return new AverageRate { NumberOfEvaluators = numberOfEvaluators, AverageValue = averageValue };

        }
    }
}
