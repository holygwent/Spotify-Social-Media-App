using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.Services.Repositories
{
    internal class SongRepository:ISongRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SongRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateSong(string songId)
        {
           await _dbContext.Songs.AddAsync(new Song { Id = songId });
           await  _dbContext.SaveChangesAsync();
        }
        public async Task<Song> GetSong(string songId)
        {
            var song = await _dbContext.Songs
                .Include(x=>x.Comments)
                     .ThenInclude(x=>x.Author)
                .Include(x => x.Comments)
                     .ThenInclude(x => x.Comments)
                .FirstOrDefaultAsync(x=>x.Id == songId);
         
            return song;
        }

        
    }
}
