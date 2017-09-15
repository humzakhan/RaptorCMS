using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public string Title { get; set; }

        public string Action { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "System Keyword")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white spaces allowed in System Keywords.")]
        public string SystemKeyword { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
