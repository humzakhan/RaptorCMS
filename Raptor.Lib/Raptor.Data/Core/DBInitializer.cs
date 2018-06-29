using Raptor.Core.Security;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Raptor.Data.Models.Configuration;

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
                DateLastLoginUtc = DateTime.UtcNow
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
    }
}
