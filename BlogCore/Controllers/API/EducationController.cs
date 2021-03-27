using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Controllers.API
{
    [Authorize]
    public class EducationController : ODataController
    {
        private readonly DatabaseContext _context;
        public EducationController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<EducationModel>>> Get()
        {
            return await _context.Education.ToListAsync();
        }

        [HttpGet]
        [Route("GetPage")]
        public async Task<ActionResult<IEnumerable<EducationModel>>> GetPage(int Page,int PageSize)
        {
            return await _context.Education.Skip((Page - 1) * PageSize).Take(PageSize).ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<EducationModel>> Get(Guid id)
        {
            EducationModel model = await _context.Education.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }

    }
}
