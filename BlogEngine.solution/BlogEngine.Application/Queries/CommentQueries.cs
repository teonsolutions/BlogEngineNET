using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using BlogEngine.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Application.Queries
{
    public class CommentQueries : ICommentQueries
    {
        private readonly BlogEngineContext _context;

        public CommentQueries(BlogEngineContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get comments by Post
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentResponse>> GetAllCommentsByPostIDAsync(int postID)
        {
            var query = _context.Comments.AsNoTracking();
            var reponse = query.Where(row => row.PostID.Equals(postID))
                               .Select(row => new CommentResponse
                               {
                                   CommentID = row.Id,
                                   PostID = row.PostID,
                                   Comments = row.Comments,
                                   CreationUser = row.CreationUser,
                                   CreationDate = row.CreationDate,
                                   CommentType = row.CommentType
                               }).ToList();
            return await Task.FromResult(reponse);
        }
    }
}
