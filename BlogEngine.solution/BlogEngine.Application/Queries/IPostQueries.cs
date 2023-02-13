using BlogEngine.Application.ViewModels;
using BlogEngine.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Application.Queries
{
    public interface IPostQueries
    {
        Task<PaginatedResponse<PostListResponse>> GetAllPostsAsync(PaginatedRequest<PostListRequest> request);
        Task<PostResponse> GetByIDAsync(int postID);

    }
}
