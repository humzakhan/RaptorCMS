using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Raptor.Data;
using Raptor.Data.Models;

namespace Raptor.Data.Migrations
{
    [DbContext(typeof(RaptorDbContext))]
    [Migration("20170715211124_RenameBlogPostsTable")]
    partial class RenameBlogPostsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Raptor.Data.Models.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentsCount");

                    b.Property<string>("CommentsStatus");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateCreatedGmt");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateModifiedGmt");

                    b.Property<string>("Excerpt");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Link")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .HasMaxLength(20);

                    b.Property<int>("PostCategoryId");

                    b.Property<int>("PostType");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("BlogPostId");

                    b.HasIndex("PostCategoryId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("Raptor.Data.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Agent")
                        .HasMaxLength(255);

                    b.Property<bool>("Approved");

                    b.Property<string>("Author")
                        .HasMaxLength(50);

                    b.Property<string>("AuthorEmail")
                        .HasMaxLength(100);

                    b.Property<string>("AuthorIp")
                        .HasMaxLength(100);

                    b.Property<string>("AuthorUrl")
                        .HasMaxLength(200);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateCreatedGmt");

                    b.Property<int>("Karma");

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Raptor.Data.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Bio");

                    b.Property<int>("CustomerType");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FacebookUrl");

                    b.Property<string>("LinkedInUrl");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("TwitterUrl");

                    b.Property<string>("Website");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Raptor.Data.Models.PostCategory", b =>
                {
                    b.Property<int>("PostCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("ParentId");

                    b.Property<string>("Slug")
                        .HasMaxLength(100);

                    b.HasKey("PostCategoryId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("Raptor.Data.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Raptor.Data.Models.Taxonomy", b =>
                {
                    b.Property<int>("TaxonomyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("TermId");

                    b.HasKey("TaxonomyId");

                    b.HasIndex("TermId");

                    b.ToTable("Taxonomies");
                });

            modelBuilder.Entity("Raptor.Data.Models.Term", b =>
                {
                    b.Property<int>("TermId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.HasKey("TermId");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("Raptor.Data.Models.TermRelationship", b =>
                {
                    b.Property<int>("ObjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TaxonomyId");

                    b.HasKey("ObjectId");

                    b.HasIndex("TaxonomyId");

                    b.ToTable("TermRelationships");
                });

            modelBuilder.Entity("Raptor.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(50);

                    b.Property<bool>("IsAllowed");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<int>("UserProfileId");

                    b.Property<string>("Username")
                        .HasMaxLength(30);

                    b.HasKey("UserId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Raptor.Data.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Birthday")
                        .HasMaxLength(15);

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(152);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("Location")
                        .HasMaxLength(20);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<string>("Website")
                        .HasMaxLength(100);

                    b.HasKey("UserProfileId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Raptor.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("RoleId1");

                    b.Property<Guid>("rowguid");

                    b.HasKey("UserId", "RoleId");

                    b.HasAlternateKey("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Raptor.Data.Models.BlogPost", b =>
                {
                    b.HasOne("Raptor.Data.Models.PostCategory", "PostCategory")
                        .WithMany("Posts")
                        .HasForeignKey("PostCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raptor.Data.Models.Comment", b =>
                {
                    b.HasOne("Raptor.Data.Models.BlogPost", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Raptor.Data.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raptor.Data.Models.Taxonomy", b =>
                {
                    b.HasOne("Raptor.Data.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raptor.Data.Models.TermRelationship", b =>
                {
                    b.HasOne("Raptor.Data.Models.Taxonomy", "Taxonomy")
                        .WithMany()
                        .HasForeignKey("TaxonomyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raptor.Data.Models.User", b =>
                {
                    b.HasOne("Raptor.Data.Models.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raptor.Data.Models.UserRole", b =>
                {
                    b.HasOne("Raptor.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId1");

                    b.HasOne("Raptor.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
