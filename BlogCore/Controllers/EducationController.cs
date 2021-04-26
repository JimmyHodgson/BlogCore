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
            return await GetModelInfo(id);
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
            return await GetModelInfo(id);
        }

        // GET: Education/Delete/5
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
