using BlogCore.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        public ManageController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("PostUserInfo")]
        public async Task<ActionResult> PostUserInfo(User model)
        {
            User user = _context.Users.FirstOrDefault();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Title = model.Title;
            user.Link = model.Link;
            user.Bio = model.Bio;
            user.PhoneNumber = model.PhoneNumber;

            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PostPasswordChange")]
        public async Task<ActionResult> PostPasswordChange(PasswordChangeModel model)
        {
            User user = _context.Users.FirstOrDefault();

            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }

        [HttpPost]
        [Route("PostConfiguration")]
        public async Task<ActionResult> PostConfiguration(HomeModel model)
        {
            HomeModel exists = _context.Home.FirstOrDefault();
            if (exists == null)
            {
                //create it
                await _context.Home.AddAsync(model);
            }
            else
            {
                //update it
                exists.LandingImage = model.LandingImage;
                exists.SkillImage = model.SkillImage;
                exists.ContactImage = model.ContactImage;
                exists.LinkedInLink = model.LinkedInLink;
                exists.GithubLink = model.GithubLink;
                exists.CVLink = model.CVLink;

                _context.Home.Update(exists);
            }

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
    }
}