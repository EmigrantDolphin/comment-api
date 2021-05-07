using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using Application.Models;
using Persistence;

namespace Application.Services
{
    public interface ICommentService {
        Task<List<CommentModel>> GetAllCommentsAsync(string userId);
        Task AddCommentAsync(string comment, string userId);
    }

    public class CommentService : ICommentService
    {
        private readonly CommentContext _context;

        public CommentService(CommentContext context)
        {
            _context = context;
        }

        public async Task<List<CommentModel>> GetAllCommentsAsync(string userId){
            var comments = await _context.Comments
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            var commentModels = comments.Select(x => new CommentModel
            {
                CommentId = x.CommentId,
                Comment = x.Value,
                AuthorFullName = x.User.FullName,
                CanEdit = !string.IsNullOrEmpty(userId) && x.UserId.ToString().Equals(userId)
            }).ToList();

            return commentModels;
        }

        public async Task AddCommentAsync(string comment, string userId)
        {
            var commentEntity = new Comment(comment, Guid.Parse(userId));

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
        }
    }
}