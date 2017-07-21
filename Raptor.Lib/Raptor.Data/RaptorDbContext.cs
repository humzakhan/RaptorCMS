using Microsoft.EntityFrameworkCore;
using Raptor.Data.Configuration;
using Raptor.Data.Models.Blog;
using Raptor.Data.Models.Content;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;

namespace Raptor.Data
{
    public class RaptorDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseNpgsql("Host=localhost;Database=RaptorCMS;Username=root;Password=klmn256;");

            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<BlogPost>().ToTable("BlogPosts").HasKey(b => b.BlogPostId);
            modelBuilder.Entity<PersonRole>().HasKey(r => new { r.RoleId, r.BusinessEntityId });
            modelBuilder.Entity<BusinessEntityAddress>().HasKey(e => new { e.BusinessEntityId, e.AddressId });
            modelBuilder.Entity<TermRelationship>().HasKey(e => new { e.ObjectId, e.TaxonomyId });

            modelBuilder.Entity<BlogPost>(b => {
                b.Property(u => u.Guid).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<PersonRole>(p => {
                p.Property(u => u.RowGuid).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Password>(p => {
                p.Property(u => u.RowGuid).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<BusinessEntity>(p => {
                p.Property(u => u.RowGuid).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<TermRelationship>(p => {
                p.Property(u => u.RowGuid).HasDefaultValueSql("uuid_generate_v4()");
            });

            base.OnModelCreating(modelBuilder);
        }

        // Content
        public DbSet<Term> Terms { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<TermRelationship> TermRelationships { get; set; }

        // Blogs
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        // Logging
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }

        // Users
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }

        // Configuration
        public DbSet<Setting> Settings { get; set; }
    }
}
