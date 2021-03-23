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
    public class AchievementController : Controller
    {
        private readonly DatabaseContext _context;

        public AchievementController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<AchievementModel> Get()
        {
            return _context.Achievements;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<AchievementModel>> Get(Guid id)
        {
            AchievementModel model = await _context.Achievements.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }
    }
}
