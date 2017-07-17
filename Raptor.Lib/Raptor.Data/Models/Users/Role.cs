using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string SystemKeyword { get; set; }
        public string DisplayName { get; set; }
    }
}
