using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models.ViewModels;
using BlogCore.Models.Common;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;

        public HomeController(DatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

            HomeModel defaultHome = new HomeModel()
            {
                ContactImage = "",
                CVLink = "",
                GithubLink = "",
                LandingImage = "",
                LinkedInLink = "",
                SkillImage = ""
            };

            HomeViewModel model = new HomeViewModel
            {
                User = _context.Users.AsEnumerable().DefaultIfEmpty(defaultUser).First(),
                Achievements = _context.Achievements.ToList(),
                Education = _context.Education.ToList(),
                Jobs = _context.Jobs.ToList(),
                Skills = _context.Skills.ToList(),
                CaptchaClientKey = _config["captcha:SiteKey"],
                Home = _context.Home.AsEnumerable().DefaultIfEmpty(defaultHome).First()
            };

            return View(model);
        }

        public IActionResult ContentNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
