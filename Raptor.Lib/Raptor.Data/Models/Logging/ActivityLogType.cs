using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Logging
{
    public class ActivityLogType
    {
        [Key]
        public int ActivityLogTypeId { get; set; }
        public string SystemKeyword { get; set; }
        public string DisplayName { get; set; }
        public bool Enabled { get; set; }
    }
}
