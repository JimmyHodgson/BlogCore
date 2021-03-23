using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        private readonly DatabaseContext _context;
        public SkillController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<SkillModel> Get()
        {
            return _context.Skills;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<SkillModel>> Get(Guid id)
        {
            SkillModel model = await _context.Skills.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }
    }
}
