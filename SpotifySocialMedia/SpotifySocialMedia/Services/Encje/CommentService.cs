﻿using Database.Entities;
using Microsoft.AspNetCore.Identity;
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
    public class CommentService: ICommentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentService(ApplicationDbContext dbContext,UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<string> CreateComment(string username, string message, string songId)
        {
            string commentId;
           var user =  _userManager.FindByEmailAsync(username).Result;
            if (user is not null)
            {
                commentId = Guid.NewGuid().ToString();
                await _dbContext.Comments.AddAsync(new Comment { Id = commentId, AuthorId = user.Id, SongId = songId, Content = message, CreatedOn = DateTime.Now, ParentId = null });
                await _dbContext.SaveChangesAsync();
                return commentId;
            }
            else
            {
                return "";
            }
        }
        public async Task<string> CreateReply(string username, string message, string songId,string parent)
        {
            string commentId;
            var user = _userManager.FindByEmailAsync(username).Result;
            if (user is not null)
            {
                commentId = Guid.NewGuid().ToString();
                await _dbContext.Comments.AddAsync(new Comment { Id = commentId, AuthorId = user.Id, SongId = songId, Content = message, CreatedOn = DateTime.Now, ParentId = parent });
                await _dbContext.SaveChangesAsync();
                return commentId;
            }
            else
            {
                return "";
            }
        }

        public async Task<CommentAuthor> GetCommentAuthor( string parentComment)
        {
            var comment = _dbContext.Comments.Include(x=>x.Author).SingleOrDefault(x => x.Id == parentComment);
            if (comment is not null)
            {
                return new CommentAuthor
                {
                    
                    AuthorId = comment.AuthorId,
                    AuthorEmail = comment.Author.Email

                };
            }
            else
                return new CommentAuthor ();
        }

    }
}
