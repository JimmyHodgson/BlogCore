using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize(Roles = "Administrator")]
    [ODataRoutePrefix("Job")]
    public class JobController : ODataController
    {
        private readonly DatabaseContext _context;
        public JobController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [EnableQuery]
        public IQueryable<JobModel> Get()
        {
            return _context.Jobs;
        }

        // GET api/<controller>(5)
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<JobModel> Get(Guid id)
        {
            return SingleResult.Create(_context.Jobs.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]JobModel model)
        {
            try
            {
                await _context.Jobs.AddAsync(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ODataRoute("({Id})")]
        public async Task<ActionResult> Delete(Guid id)
        {
            JobModel model = _context.Jobs.FirstOrDefault(x => x.Id == id);
            if(model == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Jobs.Remove(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ODataRoute("({Id})")]
        public async Task<ActionResult> Put(Guid id,[FromBody]JobModel model)
        {
            JobModel original = _context.Jobs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(original == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Jobs.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
