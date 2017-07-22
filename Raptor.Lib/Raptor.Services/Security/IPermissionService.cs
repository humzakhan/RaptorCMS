using Raptor.Data.Models.Security;
using Raptor.Data.Models.Users;
using System.Collections.Generic;

namespace Raptor.Services.Security
{
    public interface IPermissionService
    {
        /// <summary>
        /// Creates a new permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be created</param>
        void InsertPermissionRecord(PermissionRecord permissionRecord);

        /// <summary>
        /// Updates an existing permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be updated</param>
        void UpdatePermissionRecord(PermissionRecord permissionRecord);

        /// <summary>
        /// Gets all existing permission records
        /// </summary>
        /// <returns>A list of all existing permission records</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

        /// <summary>
        /// Gets a permission record by id
        /// </summary>
        /// <param name="id">Id of the permission record</param>
        /// <returns>Permission record associated with the id</returns>
        PermissionRecord GePermissionRecordById(int id);

        /// <summary>
        /// Gets a permission record by system name
        /// </summary>
        /// <param name="systemName">System name of the permission record</param>
        /// <returns>Permisson Record associated with the system name</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        /// <summary>
        /// Deletes a permission record
        /// </summary>
        /// <param name="permissionRecord">Permission record to be deleted</param>
        void DeletePermissionRecord(PermissionRecord permissionRecord);

        /// <summary>
        /// Authorize permission for provided person
        /// </summary>
        /// <param name="permissionSystemName">System name of the permission record</param>
        /// <param name="person">Person/User to authorize</param>
        /// <returns>True - if permission is valid for specified person; false otherwise</returns>
        bool Authorize(string permissionSystemName, Person person);

        /// <summary>
        /// Authorize permission for provided person
        /// </summary>
        /// <param name="permission">Permision to validate</param>
        /// <param name="person">Person/User to authorize</param>
        /// <returns>True - if permission is valid for specified person; false otherwise</returns>
        bool Authorize(PermissionRecord permission, Person person);
    }
}
