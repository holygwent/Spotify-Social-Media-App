using Database.Entities;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.Services.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
    

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
         
        }
        public async Task CreateComment(string username, string message, string songId)
        {
            //await _dbContext.Comments.AddAsync();
            //await _dbContext.SaveChangesAsync();
        }
    }
}
