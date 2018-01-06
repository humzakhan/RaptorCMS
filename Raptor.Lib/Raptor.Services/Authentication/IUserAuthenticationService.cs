using Raptor.Data.Models.Users;

namespace Raptor.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Sign in a user
        /// </summary>
        /// <param name="usernameOEmailAddress">Username or Email Address of the user.</param>
        void SignIn(string usernameOEmailAddress);

        /// <summary>
        /// Signs out the currently logged in user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Returns the currently logged in user
        /// </summary>
        /// <returns></returns>
        Person GetAuthenticatedUser();
    }
}
