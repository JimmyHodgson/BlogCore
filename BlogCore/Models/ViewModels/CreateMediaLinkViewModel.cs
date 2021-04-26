using Microsoft.AspNetCore.Http;
using System;

namespace BlogCore.Models.ViewModels
{
    public class CreateMediaLinkViewModel
    {
        public string Name { get; set; }
        public Guid Group { get; set; }
        public IFormFile File { get; set; }
    }
}
