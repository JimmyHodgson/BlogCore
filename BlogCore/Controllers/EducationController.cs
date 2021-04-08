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
        [HttpGet("{id}")]
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

        // GET: Education/Edit/5
        [HttpGet("{id}")]
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

        // GET: Education/Delete/5
        [HttpGet("{id}")]
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
    }
}
