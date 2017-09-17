using Raptor.Data.Models.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class LogViewModel
    {
        [Display(Name = "Created From")]
        public string DateFrom { get; set; }

        [Display(Name = "Created To")]
        public string DateTo { get; set; }

        [Display(Name = "Log Level")]
        public LogLevel LogLevel { get; set; }
        public List<Log> Logs { get; set; }
    }
}
