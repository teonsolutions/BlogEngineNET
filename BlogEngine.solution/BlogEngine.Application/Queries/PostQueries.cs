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
    public class PostQueries : IPostQueries
    {
        private readonly BlogEngineContext _context;

        public PostQueries(BlogEngineContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all the posts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaginatedResponse<PostListResponse>> GetAllPostsAsync(PaginatedRequest<PostListRequest> request)
        {
            var query = _context.Posts.AsNoTracking();
            var status = _context.Status.AsNoTracking();
            var rolStatus = _context.RolStatus.AsNoTracking();

            int rolID = 0;
            var rol = _context.Rols.AsNoTracking().Where(x => x.Guid.Equals(request.Filter.RolGuid)).FirstOrDefault();
            if (rol != null)
                rolID = rol.Id;

            bool showOnlyMyPosts = rolID == RolesEnum.WRITER.GetHashCode(); // Business logic: writers only can display the their posts
            if (!showOnlyMyPosts)
                request.Filter.UserName = null;

            // filter only allowed status for role
            var rolStatusAllowd = rolStatus.Where(x => x.RolID == rolID).ToList();
            int totalPages = 0;

            var result = (from p in query
                          join s in status on p.Status equals s.Id
                          join seg in rolStatusAllowd on p.Status equals seg.StatusID
                          where ((p.CreationUser == request.Filter.UserName && showOnlyMyPosts) || (request.Filter.UserName == null && !showOnlyMyPosts))
                          select new PostListResponse
                          {
                              PostID = p.Id,
                              Title = p.Title,
                              Description = p.Description,
                              Author = p.CreationUser,
                              CreationDate = p.CreationDate,
                              Status = p.Status,
                              StatusDesc = s.Description,
                              PublishedDate = p.PublishedDate,

                          }).ToPagedResponse(request);

            decimal division = Convert.ToDecimal(result.Count) / Convert.ToDecimal(request.PageSize);
            totalPages = Convert.ToInt32(Math.Ceiling(division));

            var paginated = new PaginatedResponse<PostListResponse>(
                              pageIndex: result.PageIndex,
                              pageSize: result.PageSize,
                              count: result.Count,
                              data: result.Data,
                              totalPages: totalPages);

            return await Task.FromResult(paginated);
        }

        /// <summary>
        /// Get only one Post
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public async Task<PostResponse> GetByIDAsync(int postID)
        {
            var query = _context.Posts.AsNoTracking();
            var reponse = query.Where(row => row.Id.Equals(postID))
               .Select(row => new PostResponse
               {
                   PostID = row.Id,
                   Title = row.Title,
                   Description = row.Description,
                   Author = row.CreationUser,
                   Status = row.Status,
                   CreationDate = row.CreationDate
               }).FirstOrDefault();
            return await Task.FromResult(reponse);
        }
    }
}
