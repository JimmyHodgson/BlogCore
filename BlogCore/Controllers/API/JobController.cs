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
    public class JobController : Controller
    {
        private readonly DatabaseContext _context;
        public JobController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [EnableQuery]
        public IEnumerable<JobModel> Get()
        {
            return _context.Jobs;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<JobModel>> Get(Guid id)
        {
            JobModel model = await _context.Jobs.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }
    }
}
