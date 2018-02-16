using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Configuration
{
    public class Setting
    {
        public Setting() { }

        public Setting(string name, string value) {
            Name = name;
            Value = value;
        }

        [Key]
        public int SettingId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
