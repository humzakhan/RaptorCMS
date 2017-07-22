using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Security
{
    public class PermissionRecord
    {
        /// <summary>
        /// Primary key for the table
        /// </summary>
        [Key]
        public int PermissionRecordId { get; set; }

        /// <summary>
        /// Descriptive name of the permission
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// System name of the permission
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Category of the permission
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// A list of all the roles supported by this permission record
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
