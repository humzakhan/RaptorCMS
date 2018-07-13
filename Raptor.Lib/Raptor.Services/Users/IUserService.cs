using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;

namespace Raptor.Services.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if the user exists for the specified user id
        /// </summary>
        /// <param name="userId">Id of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        bool CheckIfUserExists(int userId);

        /// <summary>
        /// Checks if the user exists for the specified email addrsss
        /// </summary>
        /// <param name="emailAddress">Email Address of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        bool CheckIfUserExistsByEmail(string emailAddress);

        /// <summary>
        /// Checks if the user exists for the specified email addrsss
        /// </summary>
        /// <param name="username">Email Address of the user whos status is to be verified</param>
        /// <returns>True if the user exists, false otherwise</returns>
        bool CheckIfUserExistsByUsername(string username);

        /// <summary>
        /// Returns the users for the specified id.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User associated with the provided IDs.</returns>
        Person GetUserById(int id);

        /// <summary>
        /// Returns a list of users for the specified IDs.
        /// </summary>
        /// <param name="ids">IDs for whom users are to be returned</param>
        /// <returns>Users for the specified IDs</returns>
        IList<Person> GetUserByIds(int[] ids);

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>List of Person Object</returns>
        IEnumerable<Person> GetAllUsers();

        /// <summary>
        /// Gets a user with GUID
        /// </summary>
        /// <param name="guid">GUID for who's user is to be found</param>
        /// <returns>User who's guid was specified</returns>
        Person GetUserByGuid(Guid guid);

        /// <summary>
        /// Gets a user with GUID when it is specified in string format.
        /// </summary>
        /// <param name="guid">GUID for who's user is to be found</param>
        /// <returns>User who's guid was specified</returns>
        Person GetUserByGuid(string guid);

        /// <summary>
        /// Gets a user with email address
        /// </summary>
        /// <param name="emailAddress">Email Address of the user</param>
        /// <returns>User who's email address was specified</returns>
        Person GetUserByEmail(string emailAddress);

        /// <summary>
        /// Gets a user with username
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>User with the specified username</returns>
        Person GetUserByUsername(string username);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="person">User who is to be deleted</param>
        void DeleteUser(Person person);

        /// <summary>
        /// Deletes a user by user id
        /// </summary>
        /// <param name="businessEntityId">ID of the user which is to be deleted</param>
        void DeleteUser(int businessEntityId);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="person">User to be updated</param>
        void UpdateUser(Person person);

        /// <summary>
        /// Return the number of users
        /// </summary>
        /// <returns>Number of users registered</returns>
        int CountUsers();

        /// <summary>
        /// Updates a user password
        /// </summary>
        /// <param name="emailAddress">Email Address of the user who's password is to be updated.</param>
        /// <param name="password">New password</param>
        void UpdatePassword(string emailAddress, string password);

        /// <summary>
        /// Creates a forgot password request
        /// </summary>
        /// <param name="userId">Id of the user for whom the request is to be created</param>
        void CreateForgotPasswordRequest(int userId);

        /// <summary>
        /// Validate a forgot password request
        /// </summary>
        /// <param name="link">Link of the request</param>
        /// <returns>Whether link is valid</returns>
        bool ValidateForgotPasswordRequest(string link);

        /// <summary>
        /// Get forgot password request
        /// </summary>
        /// <param name="link">Link for which the request is to be fetched</param>
        /// <returns>Forgot Password Request</returns>
        ForgotPasswordRequest GetForgotPasswordRequest(string link);

       /// <summary>
       /// Create social profile
       /// </summary>
       /// <param name="profile">Social profile object to create</param>
        void CreateSocialProfile(SocialProfile profile);

        /// <summary>
        /// Find social profile by user id
        /// </summary>
        /// <param name="userId">Id of the user whos social profile to get</param>
        /// <returns>SocialProfile of the user</returns>
        SocialProfile GetSocialProfileById(int userId);
    }
}
