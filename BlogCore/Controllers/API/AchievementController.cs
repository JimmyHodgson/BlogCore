using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize(Roles = "Administrator")]
    [ODataRoutePrefix("Achievement")]
    public class AchievementController : ODataController
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<AchievementController> _logger;

        public AchievementController(DatabaseContext context, ILogger<AchievementController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<AchievementModel> Get()
        {
            return _context.Achievements;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<AchievementModel> Get(Guid id)
        {
            _logger.LogInformation("ID is {id}", id);
            return SingleResult.Create(_context.Achievements.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AchievementModel model)
        {
            try
            {
                await _context.Achievements.AddAsync(model);
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
            AchievementModel model = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Achievements.Remove(model);
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
        public async Task<ActionResult> Put(Guid id, [FromBody] AchievementModel model)
        {
            AchievementModel original = await _context.Achievements.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (original == null)
            {
                return NotFound($"Model with Id {id} not found.");
            }

            try
            {
                _context.Achievements.Update(model);
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
