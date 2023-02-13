using BlogEngine.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.AggregatesModel
{
    public interface IPostRepository: IGenericRepository<Post>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
