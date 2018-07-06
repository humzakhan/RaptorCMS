using Raptor.Core.Security;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Raptor.Data.Models.Configuration;
using Raptor.Data.Models.Logging;

namespace Raptor.Data.Core
{
    public static class DbInitializer
    {
        private static readonly RaptorDbContext DbContext = new RaptorDbContext();

        /// <summary>
        /// This method inserts demonstration data in to the database. When the database is plain empty,
        /// this method can be used to insert initial configuraiton
        /// </summary>
        public static void Seed() {
            // First, we create the default user
            InsertUser();

            // Now we will create some default roles
            InsertDefaultRoles();

            // Assign default user some roles
            AssignRolesToDefaultUser();

            // Insert default settings 
            InsertDefaultSettings();

            // Insert default activity log types
            InsertDefaultActivityLogTypes();
        }

        private static void InsertUser() {
            if (DbContext.People.Any()) return;

            var businessEntity = new BusinessEntity();

            DbContext.BusinessEntities.Add(businessEntity);
            DbContext.SaveChanges();

            var person = new Person() {
                BusinessEntityId = businessEntity.BusinessEntityId,
                FirstName = "Default",
                MiddleName = string.Empty,
                LastName = "Admin",
                DisplayName = "Default Admin",
                Username = "Admin",
                EmailAddress = "admin@yourdomain.com",
                About = string.Empty,
                Website = string.Empty,
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = DateTime.UtcNow,
                DateLastLoginUtc = DateTime.UtcNow,
                Avatar = $"data:image/png;base64,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"
            };

            DbContext.People.Add(person);
            DbContext.SaveChanges();

            const string plainTextPassword = "admin123";
            var passwordSalt = HashGenerator.CreateSalt();
            var passwordHash = HashGenerator.GenerateHash(plainTextPassword, passwordSalt);

            var password = new Password() {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                BusinessEntityId = person.BusinessEntityId
            };

            DbContext.Passwords.Add(password);
            DbContext.SaveChanges();
        }

        private static void InsertDefaultRoles() {
            if (DbContext.Roles.Any()) return;

            var roles = new List<Role>() {
                new Role() { SystemKeyword = "admin", DisplayName = "Administrator" },
                new Role() { SystemKeyword = "mod", DisplayName = "Moderator" },
                new Role() { SystemKeyword = "customer", DisplayName = "Customer" },
                new Role() { SystemKeyword = "user", DisplayName = "User" }
            };

            DbContext.Roles.AddRange(roles);
            DbContext.SaveChanges();
        }

        private static void AssignRolesToDefaultUser() {
            if (DbContext.PersonRoles.Any()) return;

            var roleAdmin = DbContext.Roles.SingleOrDefault(r => r.SystemKeyword == "admin");
            var roleMod = DbContext.Roles.SingleOrDefault(r => r.SystemKeyword == "mod");
            var roleUser = DbContext.Roles.SingleOrDefault(r => r.SystemKeyword == "user");

            var defaultUser = DbContext.People.SingleOrDefault(u => u.Username == "Admin");

            var userRoles = new List<PersonRole>() {
                new PersonRole() { Person = defaultUser, Role = roleAdmin },
                new PersonRole() { Person = defaultUser, Role = roleMod },
                new PersonRole() { Person = defaultUser, Role = roleUser }
            };

            DbContext.PersonRoles.AddRange(userRoles);
            DbContext.SaveChanges();
        }

        private static void InsertDefaultSettings()
        {
            var settings = new List<Setting>()
            {
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "site_name", Value = "RaptorCMS" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "site_description", Value = "A lightweight CMS built with ASP.NET Core 2.0 and PostgreSQL" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "site_registration_enabled", Value = "true" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "site_comments_enabled", Value = "true" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "default_user_role", Value = "1" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "facebook_url", Value = "https://facebook.com/" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "instagram_url", Value = "https://instagram.com/" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "twitter_url", Value = "https://twitter.com/" },
                new Setting() {DateModifiedUtc = DateTime.UtcNow, Name = "youtube_url", Value = "https://yuotube.com/" },
            };

            DbContext.Settings.AddRange(settings);
            DbContext.SaveChanges();
        }

        private static void InsertDefaultActivityLogTypes()
        {
            var activityLogTypes = new List<ActivityLogType>()
            {
                new ActivityLogType() { DisplayName = "User Login", Enabled = true, SystemKeyword = "user.login" },
                new ActivityLogType() { DisplayName = "User Logout", Enabled = true, SystemKeyword = "user.logout" },
                new ActivityLogType() { DisplayName = "Profile Updated", Enabled = true, SystemKeyword = "user.update.profile" },
                new ActivityLogType() { DisplayName = "Password Changed", Enabled = true, SystemKeyword = "user.update.password" },
                new ActivityLogType() { DisplayName = "Password Changed", Enabled = true, SystemKeyword = "user.update.password" },
                new ActivityLogType() { DisplayName = "Add blog category", Enabled = true, SystemKeyword = "blog.category.add" },
                new ActivityLogType() { DisplayName = "Edit blog category", Enabled = true, SystemKeyword = "blog.category.edit" },
                new ActivityLogType() { DisplayName = "View blog category", Enabled = true, SystemKeyword = "blog.category.view" },
                new ActivityLogType() { DisplayName = "Add blog post", Enabled = true, SystemKeyword = "blog.post.add" },
                new ActivityLogType() { DisplayName = "View blog post", Enabled = true, SystemKeyword = "blog.post.view" },
                new ActivityLogType() { DisplayName = "Edit blog post", Enabled = true, SystemKeyword = "blog.post.edit" },
                new ActivityLogType() { DisplayName = "View logs", Enabled = true, SystemKeyword = "logs.view" },
                new ActivityLogType() { DisplayName = "Create Roles", Enabled = true, SystemKeyword = "roles.create" },
                new ActivityLogType() { DisplayName = "View Roles", Enabled = true, SystemKeyword = "roles.view" },
                new ActivityLogType() { DisplayName = "Edit Roles", Enabled = true, SystemKeyword = "roles.edit" },
                new ActivityLogType() { DisplayName = "Delete Roles", Enabled = true, SystemKeyword = "roles.delete" },
                new ActivityLogType() { DisplayName = "Update Settings", Enabled = true, SystemKeyword = "settings.update" }
            };

            DbContext.ActivityLogTypes.AddRange(activityLogTypes);
            DbContext.SaveChanges();
        }
    }
}
