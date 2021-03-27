using BlogCore.Models.Common;
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
    public class MediaLinkController
    {
        private readonly DatabaseContext _context;
        public MediaLinkController(DatabaseContext context)
        {
            _context = context;
        }
    }
}
