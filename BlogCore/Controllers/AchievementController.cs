using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models.Catalogues;
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

        // GET: Achievement/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Achievement/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievementModel = await _context.Achievements.FindAsync(id);
            if (achievementModel == null)
            {
                return NotFound();
            }
            return View(achievementModel);
        }

        // GET: Achievement/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
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
