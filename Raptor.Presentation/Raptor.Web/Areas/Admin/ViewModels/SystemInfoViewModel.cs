namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class SystemInfoViewModel
    {
        public string Version { get; set; }

        public string OperatingSystem { get; set; }

        public string AspNetVersion { get; set; }

        public string ServerTimeZone { get; set; }

        public string LocalTimeZone { get; set; }

        public string Host { get; set; }
    }
}
