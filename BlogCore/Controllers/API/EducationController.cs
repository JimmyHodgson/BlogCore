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
        public IQueryable<EducationModel> Get()
        {
            return _context.Education;
        }

        // GET api/<controller>(5)
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<EducationModel> Get(Guid id)
        {
            return SingleResult.Create(_context.Education.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EducationModel model)
        {
            try
            {
                await _context.Education.AddAsync(model);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            EducationModel model = await _context.Education.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Education.Remove(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ODataRoute("({Id})")]
        public async Task<ActionResult> Put(Guid id,[FromBody] EducationModel model)
        {
            EducationModel original = await _context.Education.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(original == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Education.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
