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
    public class SkillController : Controller
    {
        private readonly DatabaseContext _context;

        public SkillController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Skill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.ToListAsync());
        }

        // GET: Skill/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillModel = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillModel == null)
            {
                return NotFound();
            }

            return View(skillModel);
        }

        // GET: Skill/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: Skill/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillModel = await _context.Skills.FindAsync(id);
            if (skillModel == null)
            {
                return NotFound();
            }
            return View(skillModel);
        }

        // GET: Skill/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillModel = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillModel == null)
            {
                return NotFound();
            }

            return View(skillModel);
        }
    }
}
