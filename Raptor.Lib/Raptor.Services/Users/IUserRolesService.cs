using Raptor.Data.Models.Users;
using System.Collections.Generic;

namespace Raptor.Services.Users
{
    public interface IUserRolesService
    {
        /// <summary>
        /// Creates a user role object
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="systemKeyword">System Keyword for the role</param>
        void CreateUserRole(string name, string systemKeyword);

        /// <summary>
        /// Creates a user role object
        /// </summary>
        /// <param name="role">User Role object to create</param>
        void CreateUserRole(Role role);

        /// <summary>
        /// Updates a user role
        /// </summary>
        /// <param name="role">User Role object to update</param>
        void UpdateUserRole(Role role);

        /// <summary>
        /// Returns the user role for the specified id.
        /// </summary>
        /// <param name="roleId">Id of the role to get</param>
        /// <returns>PersonRole for the specified id</returns>
        Role GetUserRoleById(int roleId);

        /// <summary>
        /// Returns the user role for the specified system keyword
        /// </summary>
        /// <param name="systemKeyword">System Keyword for the role to get</param>
        /// <returns>PersonRole for the specified system keyword</returns>
        Role GetUserRoleByKeyword(string systemKeyword);

        /// <summary>
        /// Returns a list of all user roles
        /// </summary>
        /// <returns>List of PersonRole</returns>
        IEnumerable<Role> GetAllUserRoles();

        /// <summary>
        /// Returns a list of all user roles containing the specified system keyword
        /// </summary>
        /// <param name="systemKeyword">System Keyword for whom the list of role is be returned</param>
        /// <returns>List of PersonRole</returns>
        IEnumerable<Role> GetAllUserRolesFor(string systemKeyword);
    }
}
