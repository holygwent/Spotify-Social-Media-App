using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<string> CreateComment(string username, string message, string songId);
    }
}
