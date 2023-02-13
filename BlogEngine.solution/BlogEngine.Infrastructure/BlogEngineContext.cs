using Microsoft.EntityFrameworkCore;
using BlogEngine.Domain.SeedWork;
using BlogEngine.Infrastructure.EntityConfigurations;
using System.Threading;
using System.Threading.Tasks;
using BlogEngine.Domain.AggregatesModel;

namespace BlogEngine.Infrastructure
{
    public class BlogEngineContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<RolUser> RolUsers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<RolStatus> RolStatus { get; set; }
        public BlogEngineContext(DbContextOptions<BlogEngineContext> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("BlogEngineContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RolEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RolUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RolStatusEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}
