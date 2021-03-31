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
    public class MediaGroupController : Controller
    {
        private readonly DatabaseContext _context;

        public MediaGroupController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: MediaGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaGroups.ToListAsync());
        }

        // GET: MediaGroup/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGroupModel = await _context.MediaGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaGroupModel == null)
            {
                return NotFound();
            }

            return View(mediaGroupModel);
        }

        // GET: MediaGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName")] MediaGroupModel mediaGroupModel)
        {
            if (ModelState.IsValid)
            {
                mediaGroupModel.Id = Guid.NewGuid();
                _context.Add(mediaGroupModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaGroupModel);
        }

        // GET: MediaGroup/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGroupModel = await _context.MediaGroups.FindAsync(id);
            if (mediaGroupModel == null)
            {
                return NotFound();
            }
            return View(mediaGroupModel);
        }

        // POST: MediaGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,NormalizedName")] MediaGroupModel mediaGroupModel)
        {
            if (id != mediaGroupModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaGroupModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaGroupModelExists(mediaGroupModel.Id))
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
            return View(mediaGroupModel);
        }

        // GET: MediaGroup/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGroupModel = await _context.MediaGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaGroupModel == null)
            {
                return NotFound();
            }

            return View(mediaGroupModel);
        }

        // POST: MediaGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mediaGroupModel = await _context.MediaGroups.FindAsync(id);
            _context.MediaGroups.Remove(mediaGroupModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaGroupModelExists(Guid id)
        {
            return _context.MediaGroups.Any(e => e.Id == id);
        }
    }
}
