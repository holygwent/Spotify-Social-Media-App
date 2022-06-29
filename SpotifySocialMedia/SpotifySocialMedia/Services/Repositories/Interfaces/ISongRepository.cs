using Database.Entities;
using SpotifySocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task CreateSong(string songId);
        Task<Song> GetSong(string songId);
    }
}
