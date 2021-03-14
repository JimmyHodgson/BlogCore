using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models.Common;

namespace BlogCore.Controllers
{
    public class LinkController : Controller
    {
        private readonly DatabaseContext _context;

        public LinkController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Link
        public async Task<IActionResult> Index()
        {
            return View(await _context.Links.ToListAsync());
        }

        // GET: Link/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkModel = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkModel == null)
            {
                return NotFound();
            }

            return View(linkModel);
        }

        // GET: Link/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Link/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Link")] LinkModel linkModel)
        {
            if (ModelState.IsValid)
            {
                linkModel.Id = Guid.NewGuid();
                _context.Add(linkModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linkModel);
        }

        // GET: Link/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkModel = await _context.Links.FindAsync(id);
            if (linkModel == null)
            {
                return NotFound();
            }
            return View(linkModel);
        }

        // POST: Link/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Link")] LinkModel linkModel)
        {
            if (id != linkModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linkModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkModelExists(linkModel.Id))
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
            return View(linkModel);
        }

        // GET: Link/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkModel = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkModel == null)
            {
                return NotFound();
            }

            return View(linkModel);
        }

        // POST: Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var linkModel = await _context.Links.FindAsync(id);
            _context.Links.Remove(linkModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkModelExists(Guid id)
        {
            return _context.Links.Any(e => e.Id == id);
        }
    }
}
