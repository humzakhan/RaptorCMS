using Raptor.Data.Models.Users;

namespace Raptor.Services.Users
{
    public interface IUserRegisterationService
    {
        /// <summary>
        /// Register a new user (person)
        /// </summary>
        /// <param name="person">Person object to register</param>
        /// <param name="password">Password for the new user in plain text</param>
        /// <returns></returns>
        UserRegistrationResult Register(Person person, string password);

        /// <summary>
        /// Register a new user with minimal information
        /// </summary>
        /// <param name="firstName">First Name of the user</param>
        /// <param name="lastName">Last Name of the user</param>
        /// <param name="emailAddress">Email Address of the user</param>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns></returns>
        UserRegistrationResult Register(string firstName, string lastName, string emailAddress, string username, string password);

        /// <summary>
        /// Update email address of an existing user
        /// </summary>
        /// <param name="person">Person who's email address is to be updated</param>
        /// <param name="newEmailAddress">New email address for the person</param>
        /// <param name="isVerificationRequired">Whether to verify the new email address when the user logs in next time or not</param>
        void UpdateEmailAddress(Person person, string newEmailAddress, bool isVerificationRequired);

        /// <summary>
        /// Update username of a person
        /// </summary>
        /// <param name="person">Person who's username is to be updated.</param>
        /// <param name="username">New username</param>
        void UpdateUsername(Person person, string username);

        /// <summary>
        /// Validates a user.
        /// </summary>
        /// <param name="usernameOrEmail">Username of email address of the user</param>
        /// <param name="password">Password of the user in plain text format</param>
        UserLoginResults ValidateUser(string usernameOrEmail, string password);

    }
}
