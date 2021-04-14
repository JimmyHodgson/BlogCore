using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Common;
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
            User user = _context.Users.FirstOrDefault();
            return View(user);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}