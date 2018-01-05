using Raptor.Core.Helpers;
using Raptor.Core.Security;
using Raptor.Data.Core;
using Raptor.Data.Models.Users;
using System;
using System.Linq;

namespace Raptor.Services.Users
{
    public class UserRegistrationService : IUserRegisterationService
    {
        private readonly IRepository<Person> _peopleRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<Password> _passwordRepository;

        public UserRegistrationService(IRepository<Person> peopleRepository,
            IRepository<BusinessEntity> businessEntityRepository,
            IRepository<Password> passwordRepository) {
            _peopleRepository = peopleRepository;
            _businessEntityRepository = businessEntityRepository;
            _passwordRepository = passwordRepository;
        }

        /// <summary>
        /// Register a new user (person)
        /// </summary>
        /// <param name="person">Person object to register</param>
        /// <param name="password">Password for the new user in plain text</param>
        /// <returns></returns>
        public UserRegistrationResult Register(Person person, string password) {
            if (person == null)
                throw new ArgumentException("Person object cannot be null.", nameof(person));

            var result = new UserRegistrationResult();

            if (_peopleRepository.Find(p => p.EmailAddress == person.EmailAddress) != null) {
                result.AddError("A user already exists with the specified email address.");
                return result;
            }

            if (string.IsNullOrEmpty(person.EmailAddress)) {
                result.AddError("Email Address is not provided.");
                return result;
            }

            if (!CommonHelper.IsValidEmail(person.EmailAddress)) {
                result.AddError("Invalid Email Address specfied.");
                return result;
            }

            if (string.IsNullOrEmpty(password)) {
                result.AddError("A password is required.");
                return result;
            }

            if (string.IsNullOrEmpty(person.Username)) {
                result.AddError("Please enter a username.");
                return result;
            }

            if (_peopleRepository.Find(p => p.Username == person.Username) != null) {
                result.AddError("The username is not available.");
                return result;
            }

            if (person.Username.Length > 50) {
                result.AddError("The username cannot exceed 50 characters.");
                return result;
            }

            // If we made it this far, then the information provided is valid. We can proceed now.

            var businessEntity = new BusinessEntity();
            _businessEntityRepository.Create(businessEntity);

            var user = new Person() {
                BusinessEntityId = businessEntity.BusinessEntityId,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                DisplayName = $"{person.FirstName} {person.LastName}",
                About = person.About,
                Username = person.Username,
                EmailAddress = person.EmailAddress,
                Website = person.Website,
                IsDeleted = false,
                IsBlocked = false,
                IsEmailVerified = false,
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = DateTime.UtcNow,
                DateLastLoginUtc = DateTime.UtcNow
            };

            _peopleRepository.Create(user);

            var salt = HashGenerator.CreateSalt();
            var passwordSet = new Password() {
                BusinessEntityId = person.BusinessEntityId,
                PasswordSalt = salt,
                PasswordHash = HashGenerator.GenerateHash(password, salt)
            };

            _passwordRepository.Create(passwordSet);

            return result;
        }

        /// <summary>
        /// Register a new user with minimal information
        /// </summary>
        /// <param name="firstName">First Name of the user</param>
        /// <param name="lastName">Last Name of the user</param>
        /// <param name="emailAddress">Email Address of the user</param>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns></returns>
        public UserRegistrationResult Register(string firstName, string lastName, string emailAddress, string username, string password) {
            var person = new Person() {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Username = username,
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = DateTime.UtcNow,
                DateLastLoginUtc = DateTime.UtcNow
            };

            return Register(person, password);
        }

        /// <summary>
        /// Update email address of an existing user
        /// </summary>
        /// <param name="person">Person who's email address is to be updated</param>
        /// <param name="newEmailAddress">New email address for the person</param>
        /// <param name="isVerificationRequired">Whether to verify the new email address when the user logs in next time or not</param>
        public void UpdateEmailAddress(Person person, string newEmailAddress, bool isVerificationRequired) {
            if (person == null)
                throw new ArgumentException("Person object cannot be null.", nameof(person));

            if (string.IsNullOrEmpty(newEmailAddress))
                throw new ArgumentException("Email Address cannot be empty.", nameof(newEmailAddress));

            if (!CommonHelper.IsValidEmail(newEmailAddress))
                throw new ArgumentException("Email Address is not a valid email address.", nameof(newEmailAddress));

            if (_peopleRepository.Find(p => p.EmailAddress == person.EmailAddress) != null)
                throw new ArgumentException("Email Address already exists.", nameof(newEmailAddress));

            if (newEmailAddress.Length > 50)
                throw new ArgumentException("Email Address cannot be longer than 50 characters.", nameof(newEmailAddress));

            person.EmailAddress = newEmailAddress;
            person.IsEmailVerified = isVerificationRequired;

            _peopleRepository.Update(person);

        }

        /// <summary>
        /// Update username of a person
        /// </summary>
        /// <param name="person">Person who's username is to be updated.</param>
        /// <param name="username">New username</param>
        public void UpdateUsername(Person person, string username) {
            if (person == null)
                throw new ArgumentException("Person object cannot be null.", nameof(person));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Please provide a username.", nameof(person));

            if (_peopleRepository.Find(p => p.Username == username) != null)
                throw new ArgumentException("Username is not available.", nameof(person));

            if (username.Length > 50)
                throw new ArgumentException("Username cannot exceed 50 characters.", nameof(person));

            person.Username = username;
            _peopleRepository.Update(person);
        }

        /// <summary>
        /// Validates a user.
        /// </summary>
        /// <param name="usernameOrEmail">Username of email address of the user</param>
        /// <param name="password">Password of the user in plain text format</param>
        public UserLoginResults ValidateUser(string usernameOrEmail, string password) {
            if (string.IsNullOrEmpty(usernameOrEmail))
                throw new ArgumentException("A username or email address is required.", nameof(usernameOrEmail));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("A password is required.", nameof(password));

            var user = CommonHelper.IsValidEmail(usernameOrEmail)
                ? _peopleRepository.Include(p => p.Password).SingleOrDefault(p => p.EmailAddress == usernameOrEmail)
                : _peopleRepository.Include(p => p.Password).SingleOrDefault(p => p.Username == usernameOrEmail);

            if (user == null)
                return UserLoginResults.UserNotExists;

            if (user.IsDeleted)
                return UserLoginResults.Deleted;

            if (user.IsBlocked)
                return UserLoginResults.NotActive;

            var passwordHash = HashGenerator.GenerateHash(password, user.Password.PasswordSalt);

            if (passwordHash != user.Password.PasswordHash) return UserLoginResults.WrongPassword;

            user.DateLastLoginUtc = DateTime.UtcNow;
            return UserLoginResults.Successful;
        }
    }
}
