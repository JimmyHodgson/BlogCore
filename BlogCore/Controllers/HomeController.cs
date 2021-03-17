using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models.ViewModels;
using NETCore.MailKit.Core;
using Microsoft.AspNetCore.Hosting;
using BlogCore.Common;
using BlogCore.Models.Common;

namespace BlogCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            User defaultUser = new User()
            {
                FirstName = "Demo",
                LastName = "User",
                Email = "demo.user@example.com",
                PhoneNumber = "1234567890",
                Link = "/assets/dist/images/default_profile_pic.png",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };

            HomeViewModel model = new HomeViewModel();
            model.User = _context.Users.DefaultIfEmpty(defaultUser).First();
            model.Achievements = _context.Achievements.ToList();
            model.Education = _context.Education.ToList();
            model.Jobs = _context.Jobs.ToList();
            model.Skills = _context.Skills.ToList();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
