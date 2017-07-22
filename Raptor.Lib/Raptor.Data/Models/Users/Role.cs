using Raptor.Data.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string SystemKeyword { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<PersonRole> UserRoles { get; set; }
    }
}
