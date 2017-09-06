using Raptor.Data.Core;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Raptor.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<Person> _peopleRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IList<Expression<Func<Person, object>>> _userProperties;

        public UserService(IRepository<Person> peopleRepository, IRepository<BusinessEntity> businessEntityRepository) {
            _peopleRepository = peopleRepository;
            _businessEntityRepository = businessEntityRepository;

            _userProperties = new List<Expression<Func<Person, object>>> {
                u => u.Password,
                u => u.PhoneNumbers,
                u => u.UserRoles
            };

        }

        /// <summary>
        /// Checks if the user exists for the specified user id
        /// </summary>
        /// <param name="userId">Id of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        public bool CheckIfUserExists(int userId) {
            return _peopleRepository.Any(userId);
        }

        /// <summary>
        /// Checks if the user exists for the specified email addrsss
        /// </summary>
        /// <param name="emailAddress">Email Address of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        public bool CheckIfUserExistsByEmail(string emailAddress) {
            return _peopleRepository.Find(u => u.EmailAddress == emailAddress).Any();
        }

        /// <summary>
        /// Checks if the user exists for the specified email addrsss
        /// </summary>
        /// <param name="username">Email Address of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        public bool CheckIfUserExistsByUsername(string username) {
            return _peopleRepository.Find(u => u.Username == username).Any();
        }

        /// <summary>
        /// Returns the users for the specified id.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User associated with the provided IDs.</returns>
        public Person GetUserById(int id) {
            if (id == 0)
                throw new ArgumentException("User ID cannot be zero.", nameof(id));

            if (!CheckIfUserExists(id))
                throw new ArgumentException($"No user exists for the specified user id = {id}", nameof(id));

            return _peopleRepository
                .IncludeMultiple(_userProperties)
                .SingleOrDefault(u => u.BusinessEntityId == id && !u.IsDeleted);
        }

        /// <summary>
        /// Returns a list of users for the specified IDs.
        /// </summary>
        /// <param name="ids">IDs for whom users are to be returned</param>
        /// <returns>Users for the specified IDs</returns>
        public IList<Person> GetUserByIds(int[] ids) {
            if (ids == null || ids.Length == 0)
                return new List<Person>();

            return _peopleRepository.Find(u => ids.Contains(u.BusinessEntityId) && !u.IsDeleted).ToList();
        }

        /// <summary>
        /// Gets a user with GUID
        /// </summary>
        /// <param name="guid">GUID for who's user is to be found</param>
        /// <returns>User who's guid was specified</returns>
        public Person GetUserByGuid(Guid guid) {
            var businessEntity = _businessEntityRepository
                .SingleOrDefault(b => b.RowGuid == guid);

            if (businessEntity == null)
                throw new ArgumentException("No user can be found for specified guid.", nameof(guid));

            return GetUserById(businessEntity.BusinessEntityId);
        }

        /// <summary>
        /// Gets a user with GUID when it is specified in string format.
        /// </summary>
        /// <param name="guid">GUID for who's user is to be found</param>
        /// <returns>User who's guid was specified</returns>
        public Person GetUserByGuid(string guid) {
            return GetUserByGuid(Guid.Parse(guid));
        }

        /// <summary>
        /// Gets a user with email address
        /// </summary>
        /// <param name="emailAddress">Email Address of the user</param>
        /// <returns>User who's email address was specified</returns>
        public Person GetUserByEmail(string emailAddress) {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("An Email Address is required. String cannot be null/empty.", nameof(emailAddress));

            if (!CheckIfUserExistsByEmail(emailAddress))
                throw new ArgumentException("No user found for the specified email address.", nameof(emailAddress));

            var user = _peopleRepository
                .IncludeMultiple(_userProperties)
                .SingleOrDefault(u => u.EmailAddress == emailAddress);

            return user;
        }

        /// <summary>
        /// Gets a user with username
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>User with the specified username</returns>
        public Person GetUserByUsername(string username) {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Please provide a username. It cannot be empty.", nameof(username));

            var user = _peopleRepository
                .IncludeMultiple(_userProperties)
                .SingleOrDefault(u => u.Username == username);

            if (user == null)
                throw new ArgumentException($"No user found for the specified username = {username}", nameof(username));

            return user;
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="person">User who is to be deleted</param>
        public void DeleteUser(Person person) {
            if (person == null)
                throw new ArgumentException("User cannot be null.", nameof(person));

            person.IsDeleted = true;
            _peopleRepository.Update(person);
        }

        /// <summary>
        /// Deletes a user by user id
        /// </summary>
        /// <param name="businessEntityId">ID of the user which is to be deleted</param>
        public void DeleteUser(int businessEntityId) {
            if (businessEntityId == 0)
                throw new ArgumentException("Business Entity Id cannot be zero.", nameof(businessEntityId));

            var person = _peopleRepository.GetById(businessEntityId);

            person.IsDeleted = true;
            _peopleRepository.Update(person);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="person">User to be updated</param>
        public void UpdateUser(Person person) {
            if (person == null)
                throw new ArgumentException("User cannot be null.", nameof(person));

            _peopleRepository.Update(person);
        }
    }
}
