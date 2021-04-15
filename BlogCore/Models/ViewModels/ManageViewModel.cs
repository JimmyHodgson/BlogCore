using BlogCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class ManageViewModel
    {
        public User Account { get; set; }
        public HomeModel Configuration { get; set; }
        public PasswordChangeModel Security { get; set; }
    }
}
