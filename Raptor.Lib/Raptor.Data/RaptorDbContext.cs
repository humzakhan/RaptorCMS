using Microsoft.EntityFrameworkCore;
using Raptor.Data.Models;
using Raptor.Data.Models.Logging;

namespace Raptor.Data
{
    public class RaptorDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Database=RaptorCMS;Username=root;Password=klmn256;");

            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(c => new { c.UserId, c.RoleId });
            modelBuilder.Entity<BlogPost>().ToTable("BlogPosts").HasKey(b => b.BlogPostId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<TermRelationship> TermRelationships { get; set; }
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Logging
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }
    }
}
