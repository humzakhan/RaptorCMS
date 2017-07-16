using Microsoft.EntityFrameworkCore;
using Raptor.Data.Models;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;

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
            modelBuilder.Entity<BlogPost>().ToTable("BlogPosts").HasKey(b => b.BlogPostId);
            modelBuilder.Entity<BusinessEntityAddress>().HasKey(e => new { e.BusinessEntityId, e.AddressId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<TermRelationship> TermRelationships { get; set; }

        //Blogs
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        // Logging
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }

        //Users
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    }
}
