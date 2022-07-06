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
    internal class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;

        public SongRepository(ApplicationDbContext dbContext, IMapper mapper,
             IArtistRepository artistRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task CreateSong(string songId)
        {
            var songInfo = _artistRepository.AddArtist(songId).Result;


            await _dbContext.Songs.AddAsync(new Song { Id = songId, ArtistId = songInfo.ArtistId, Name = songInfo.SongName });

            await _dbContext.SaveChangesAsync();
        }
        public async Task<Song> GetSong(string songId)
        {
            var song = await _dbContext.Songs
                .Include(x => x.Comments)
                     .ThenInclude(x => x.Author)
                .Include(x => x.Comments)
                     .ThenInclude(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == songId);

            return song;
        }


    }
}
