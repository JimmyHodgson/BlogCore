using BlogCore.Models.Common;

namespace BlogCore.Models.ViewModels
{
    public class ManageViewModel
    {
        public User Account { get; set; }
        public HomeModel Configuration { get; set; }
        public PasswordChangeModel Security { get; set; }
    }
}
