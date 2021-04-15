using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Common;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Controllers
{
    [Authorize (Roles = "Administrator")]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly DatabaseContext _context;
        public ManageController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ManageViewModel model = new ManageViewModel
            {
                Account = _context.Users.FirstOrDefault(),
                Configuration = _context.Home.FirstOrDefault(),
                Security = new PasswordChangeModel()
            };
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}