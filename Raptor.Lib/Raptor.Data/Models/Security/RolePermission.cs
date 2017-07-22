using Raptor.Data.Models.Users;

namespace Raptor.Data.Models.Security
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int PermissionRecordId { get; set; }
        public virtual PermissionRecord PermissionRecord { get; set; }
    }
}
