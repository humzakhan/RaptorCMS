using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Logging
{
    public class ActivityLog
    {
        [Key]
        public int ActivityLogId { get; set; }
        public int CustomerId { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreatedUtc { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
