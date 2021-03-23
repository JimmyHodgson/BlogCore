using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BlogCore.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    public class EducationController : Controller
    {
        private readonly DatabaseContext _context;
        public EducationController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        [EnableQuery]
        public IEnumerable<EducationModel> Get()
        {
            return _context.Education;
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
