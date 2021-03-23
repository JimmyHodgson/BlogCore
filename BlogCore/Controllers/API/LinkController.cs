using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogCore.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    public class LinkController : Controller
    {
        private readonly DatabaseContext _context;

        public LinkController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [EnableQuery]
        public IEnumerable<LinkModel> Get()
        {
            return _context.Links;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinkModel>> Get(Guid id)
        {
            LinkModel model = await _context.Links.FindAsync(id);
            if (model == null) return NotFound();
            return model;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post(LinkModel link)
        {
            await _context.Links.AddAsync(link);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, LinkModel link)
        {
            LinkModel model = await _context.Links.FindAsync(id);
            if (model == null) return NotFound();

            _context.Links.Update(link);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            LinkModel model = await _context.Links.FindAsync(id);
            if (model == null) return NotFound();

            _context.Links.Remove(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }
    }
}
