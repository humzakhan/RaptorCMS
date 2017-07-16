using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
