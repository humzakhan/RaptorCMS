using Raptor.Data.Core;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;

namespace Raptor.Services.Users
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IRepository<Role> _rolesRepository;


        public UserRolesService(IRepository<Role> rolesRepository) {
            _rolesRepository = rolesRepository;
        }

        /// <summary>
        /// Creates a user role object
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="systemKeyword">System Keyword for the role</param>
        public void CreateUserRole(string name, string systemKeyword) {
            var role = new Role() {
                DisplayName = name,
                SystemKeyword = systemKeyword
            };

            CreateUserRole(role);
        }

        /// <summary>
        /// Creates a user role object
        /// </summary>
        /// <param name="role">User Role object to create</param>
        public void CreateUserRole(Role role) {
            var checkRole = GetUserRoleByKeyword(role.SystemKeyword);
            if (checkRole != null) throw new ArgumentException($"A role already exists for the specified system keyword: {role.SystemKeyword}");

            _rolesRepository.Create(role);
        }

        /// <summary>
        /// Updates a user role
        /// </summary>
        /// <param name="role">User Role object to update</param>
        public void UpdateUserRole(Role role) {
            _rolesRepository.Update(role);
        }

        /// <summary>
        /// Returns the user role for the specified id.
        /// </summary>
        /// <param name="roleId">Id of the role to get</param>
        /// <returns>PersonRole for the specified id</returns>
        public Role GetUserRoleById(int roleId) {
            return _rolesRepository.GetById(roleId);
        }

        /// <summary>
        /// Returns the user role for the specified system keyword
        /// </summary>
        /// <param name="systemKeyword">System Keyword for the role to get</param>
        /// <returns>PersonRole for the specified system keyword</returns>
        public Role GetUserRoleByKeyword(string systemKeyword) {
            return _rolesRepository.SingleOrDefault(r => r.SystemKeyword == systemKeyword);
        }

        /// <summary>
        /// Returns a list of all user roles
        /// </summary>
        /// <returns>List of PersonRole</returns>
        public IEnumerable<Role> GetAllUserRoles() {
            return _rolesRepository.GetAll();
        }

        /// <summary>
        /// Returns a list of all user roles containing the specified system keyword
        /// </summary>
        /// <param name="systemKeyword">System Keyword for whom the list of role is be returned</param>
        /// <returns>List of PersonRole</returns>
        public IEnumerable<Role> GetAllUserRolesFor(string systemKeyword) {
            return _rolesRepository.Find(r => r.SystemKeyword.Contains(systemKeyword));
        }
    }
}
