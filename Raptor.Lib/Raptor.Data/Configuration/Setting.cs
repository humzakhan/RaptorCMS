using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Configuration
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateModifiedUtc { get; set; }
    }
}
