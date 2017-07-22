using Raptor.Data.Core;
using Raptor.Data.Models.Security;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Security
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<PermissionRecord> _permissionRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;

        public PermissionService(IRepository<PermissionRecord> permissionRepository, IRepository<RolePermission> rolePermissionRepository) {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// Creates a new permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be created</param>
        public void InsertPermissionRecord(PermissionRecord permissionRecord) {
            if (permissionRecord == null)
                throw new ArgumentException("Permission record cannot be null.", nameof(permissionRecord));

            _permissionRepository.Create(permissionRecord);
        }

        /// <summary>
        /// Updates an existing permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be updated</param>
        public void UpdatePermissionRecord(PermissionRecord permissionRecord) {
            if (permissionRecord == null)
                throw new ArgumentException("Permission record cannot be null.", nameof(permissionRecord));

            if (!_permissionRepository.Any(permissionRecord.PermissionRecordId))
                throw new ArgumentException($"No permission record exists for id = {permissionRecord.PermissionRecordId}", nameof(permissionRecord.PermissionRecordId));

            _permissionRepository.Update(permissionRecord);
        }

        /// <summary>
        /// Gets all existing permission records
        /// </summary>
        /// <returns>A list of all existing permission records</returns>
        public IList<PermissionRecord> GetAllPermissionRecords() {
            return _permissionRepository.GetAll().ToList();
        }

        /// <summary>
        /// Gets a permission record by id
        /// </summary>
        /// <param name="id">Id of the permission record</param>
        /// <returns>Permission record associated with the id</returns>
        public PermissionRecord GePermissionRecordById(int id) {
            if (id == 0)
                throw new ArgumentException("Permission record id cannot be zero.", nameof(id));

            if (!_permissionRepository.Any(id))
                throw new ArgumentException($"No permission record exists for id = {id}", nameof(id));

            return _permissionRepository
                .Include(p => p.RolePermissions)
                .SingleOrDefault(p => p.PermissionRecordId == id);
        }

        /// <summary>
        /// Gets a permission record by system name
        /// </summary>
        /// <param name="systemName">System name of the permission record</param>
        /// <returns>Permisson Record associated with the system name</returns>
        public PermissionRecord GetPermissionRecordBySystemName(string systemName) {
            if (string.IsNullOrEmpty(systemName))
                throw new ArgumentException("Permission record system name cannot be null or empty.", nameof(systemName));

            var permissionRecord = _permissionRepository
                .Include(p => p.RolePermissions)
                .SingleOrDefault(p => p.SystemName == systemName);

            if (permissionRecord == null)
                throw new ArgumentException($"No permission record found for specified system name = {systemName}", nameof(systemName));

            return permissionRecord;
        }

        /// <summary>
        /// Deletes a permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be deleted</param>
        public void DeletePermissionRecord(PermissionRecord permissionRecord) {
            if (permissionRecord == null)
                throw new ArgumentException("Permission record cannot be null.", nameof(permissionRecord));

            if (!_permissionRepository.Any(permissionRecord.PermissionRecordId))
                throw new ArgumentException($"No permission record exists for id = {permissionRecord.PermissionRecordId}", nameof(permissionRecord.PermissionRecordId));

            _permissionRepository.Delete(permissionRecord);
        }

        /// <summary>
        /// Authorize permission for provided person
        /// </summary>
        /// <param name="permissionSystemName">System name of the permission record</param>
        /// <param name="person">Person/User to authorize</param>
        /// <returns>True - if permission is valid for specified person; false otherwise</returns>
        public bool Authorize(string permissionSystemName, Person person) {
            if (string.IsNullOrEmpty(permissionSystemName))
                return false;

            if (person == null)
                return false;

            var permissionRecord = GetPermissionRecordBySystemName(permissionSystemName);
            var personRoleIds = person.UserRoles.Select(r => r.RoleId).ToArray();

            return permissionRecord.RolePermissions.Any(p => personRoleIds.Contains(p.RoleId));
        }

        /// <summary>
        /// Authorize permission for provided person
        /// </summary>
        /// <param name="permission">Permision to validate</param>
        /// <param name="person">Person/User to authorize</param>
        /// <returns>True - if permission is valid for specified person; false otherwise</returns>
        public bool Authorize(PermissionRecord permission, Person person) {
            return Authorize(permission.SystemName, person);
        }
    }
}
