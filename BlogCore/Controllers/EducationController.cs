using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;

namespace BlogCore.Controllers
{
    public class EducationController : Controller
    {
        private readonly DatabaseContext _context;

        public EducationController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Education
        public async Task<IActionResult> Index()
        {
            return View(await _context.Education.ToListAsync());
        }

        // GET: Education/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationModel == null)
            {
                return NotFound();
            }

            return View(educationModel);
        }

        // GET: Education/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Education/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,School,Link,Title,ImageUrl,Year")] EducationModel educationModel)
        {
            if (ModelState.IsValid)
            {
                educationModel.Id = Guid.NewGuid();
                _context.Add(educationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationModel);
        }

        // GET: Education/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education.FindAsync(id);
            if (educationModel == null)
            {
                return NotFound();
            }
            return View(educationModel);
        }

        // POST: Education/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,School,Link,Title,ImageUrl,Year")] EducationModel educationModel)
        {
            if (id != educationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationModelExists(educationModel.Id))
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
            return View(educationModel);
        }

        // GET: Education/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationModel == null)
            {
                return NotFound();
            }

            return View(educationModel);
        }

        // POST: Education/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var educationModel = await _context.Education.FindAsync(id);
            _context.Education.Remove(educationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationModelExists(Guid id)
        {
            return _context.Education.Any(e => e.Id == id);
        }
    }
}
