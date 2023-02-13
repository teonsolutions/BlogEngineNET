using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Application.Queries
{
    public interface ICommentQueries
    {
        Task<IEnumerable<CommentResponse>> GetAllCommentsByPostIDAsync(int postID);

    }
}
