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
            return await GetModelInfo(id);
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
            return await GetModelInfo(id);
        }

        // GET: Skill/Delete/5
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
