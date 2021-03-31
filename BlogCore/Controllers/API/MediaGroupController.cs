﻿using BlogCore.Models.Catalogues;
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
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MediaGroupModel model)
        {
            MediaGroupModel exist = await _context.MediaGroups.FirstOrDefaultAsync(x => x.NormalizedName == model.NormalizedName);
            if (exist != null)
            {
                return BadRequest("Media group name already exists.");
            }

            await _context.MediaGroups.AddAsync(model);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
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
                return NotFound();
            }

            _context.MediaGroups.Remove(model);

            try
            {
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]MediaGroupModel model)
        {
            MediaGroupModel original = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (original == null)
            {
                return NotFound();
            }

            try
            {
                _context.MediaGroups.Update(model);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}