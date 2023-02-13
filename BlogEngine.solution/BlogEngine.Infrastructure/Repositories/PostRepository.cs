using BlogEngine.Domain.AggregatesModel;
using BlogEngine.Domain.SeedWork;
using System;

namespace BlogEngine.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogEngineContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
