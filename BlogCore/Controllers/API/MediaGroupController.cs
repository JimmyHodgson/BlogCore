using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize]
    [ODataRoutePrefix("MediaGroup")]
    public class MediaGroupController : ODataController
    {
        public readonly DatabaseContext _context;
        public MediaGroupController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<MediaGroupModel>>> Get()
        {
            return await _context.MediaGroups.ToListAsync();
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public async Task<ActionResult<MediaGroupModel>> Get(Guid id)
        {
            MediaGroupModel model = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound($"Media Group with id '${id}' not found.");
            }

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MediaGroupModel model)
        {
            MediaGroupModel exist = await _context.MediaGroups.FirstOrDefaultAsync(x => x.NormalizedName == model.NormalizedName);
            if (exist != null)
            {
                return BadRequest($"Media group '{model.Name}' already exists.");
            }

            await _context.MediaGroups.AddAsync(model);
            try
            {
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
        public async Task<ActionResult> Delete([FromODataUri]Guid id)
        {
            MediaGroupModel model = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound($"Media Group with id '${id}' not found.");
            }
            //TODO
            //Remove all images in this mediagroup


            _context.MediaGroups.Remove(model);

            try
            {
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
        public async Task<ActionResult> Put([FromODataUri]Guid id,[FromBody]MediaGroupModel model)
        {
            MediaGroupModel original = await _context.MediaGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (original == null)
            {
                return NotFound($"Media Group with Name '{model.Name}' not found.");
            }

            try
            {
                var exists = await _context.MediaGroups.FirstOrDefaultAsync(x => x.NormalizedName == model.NormalizedName && x.Id != model.Id);
                if (exists != null)
                {
                    return BadRequest($"Media Group with Name {model.Name} already exists.");
                }
                //TODO
                //Move all the images to the updated mediagroup
                _context.MediaGroups.Update(model);
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
