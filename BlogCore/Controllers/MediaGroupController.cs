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
                ModelState.TryAddModelError("ServerError", $"Media Group Id is required.");
                return NotFound();
            }

            var mediaGroupModel = await _context.MediaGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaGroupModel == null)
            {
                ModelState.TryAddModelError("ServerError", $"Media Group with Id {id} not found.");
                return NotFound();
            }

            return View(mediaGroupModel);
        }

        // GET: MediaGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: MediaGroup/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                ModelState.TryAddModelError("ServerError", $"Media Group Id is invalid.");
                return View();
            }

            var mediaGroupModel = await _context.MediaGroups.FindAsync(id);
            if (mediaGroupModel == null)
            {
                ModelState.TryAddModelError("ServerError", $"Media Group with Id {id} not found.");
                return View();
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

    }
}
