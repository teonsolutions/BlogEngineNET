using BlogEngine.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace BlogEngine.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BlogEngineContext _context;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(BlogEngineContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public BlogEngineContext Context
        {
            get { return _context; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_context != null)
                _context.Dispose();

        }
        public GenericRepository<T> GenericRepository<T>() where T : Entity
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _context.SaveChanges();
            return await Task.FromResult(0);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _context.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}
