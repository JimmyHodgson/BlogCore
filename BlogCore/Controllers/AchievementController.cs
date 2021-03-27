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

        // POST: Achievement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Type,Year")] AchievementModel achievementModel)
        {
            if (ModelState.IsValid)
            {
                achievementModel.Id = Guid.NewGuid();
                _context.Add(achievementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achievementModel);
        }

        // GET: Achievement/Edit/5
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

        // POST: Achievement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Type,Year")] AchievementModel achievementModel)
        {
            if (id != achievementModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievementModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementModelExists(achievementModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(achievementModel);
        }

        // GET: Achievement/Delete/5
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

        // POST: Achievement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var achievementModel = await _context.Achievements.FindAsync(id);
            _context.Achievements.Remove(achievementModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementModelExists(Guid id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }
    }
}
