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
    [ODataRoutePrefix("Skill")]
    public class SkillController : ODataController
    {
        private readonly DatabaseContext _context;
        public SkillController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<SkillModel> Get()
        {
            return _context.Skills;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<SkillModel> Get(Guid id)
        {
            return SingleResult.Create(_context.Skills.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]SkillModel model)
        {
            try
            {
                await _context.Skills.AddAsync(model);
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
            SkillModel model = await _context.Skills.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Skills.Remove(model);
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
        public async Task<ActionResult> Put(Guid id, [FromBody]SkillModel model)
        {
            SkillModel original = await _context.Skills.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (original == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Skills.Update(model);
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
