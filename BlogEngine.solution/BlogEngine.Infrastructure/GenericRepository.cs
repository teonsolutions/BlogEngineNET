using BlogEngine.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Domain.AggregatesModel;

namespace BlogEngine.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : Entity
    {
        internal BlogEngineContext _context;

        public GenericRepository(UnitOfWork unitOfWork) : this(unitOfWork.Context)
        {
        }
        public GenericRepository(BlogEngineContext context)
        {            
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }
        public T Update(T entity)
       {
            return _context.Set<T>().Update(entity).Entity;
        }

        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }

        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool asNoTracking = false)
        {   
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = asNoTracking ? query.AsNoTracking().Where(filter) : query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public T GetById(int Id)
        {
            return _context.Set<T>().Find(Id);
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

    }
}
