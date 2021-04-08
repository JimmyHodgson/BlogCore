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
    public class JobController : Controller
    {
        private readonly DatabaseContext _context;

        public JobController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Job
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jobs.ToListAsync());
        }

        // GET: Job/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: Job/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.Jobs.FindAsync(id);
            if (jobModel == null)
            {
                return NotFound();
            }
            return View(jobModel);
        }


        // GET: Job/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

    }
}
