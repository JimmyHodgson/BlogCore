using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models.Common;
using Microsoft.AspNetCore.Authorization;

namespace BlogCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("[controller]/[action]")]
    public class AchievementController : Controller
    {
        private readonly DatabaseContext _context;

        public AchievementController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Achievement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Achievements.ToListAsync());
        }

        // GET: Achievement/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            return await GetModelInfo(id);
        }

        // GET: Achievement/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Achievement/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return await GetModelInfo(id);
        }

        // GET: Achievement/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return await GetModelInfo(id);
        }

        private async Task<IActionResult> GetModelInfo(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievementModel = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievementModel == null)
            {
                return NotFound();
            }

            return View(achievementModel);
        }
    }
}
