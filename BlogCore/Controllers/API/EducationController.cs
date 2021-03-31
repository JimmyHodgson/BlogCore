using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Controllers.API
{
    [Authorize]
    [ODataRoutePrefix("Education")]
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

        // GET api/<controller>(5)
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public async Task<ActionResult<EducationModel>> Get(Guid id)
        {
            EducationModel model = await _context.Education.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }

    }
}
