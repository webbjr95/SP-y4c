using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Areas.Identity.Data;
using SP_Y4C.Data;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AccountController : Controller
    {
        private readonly UserDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _dbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var allRoles = await _dbContext.Roles.ToListAsync();
            var userRoleId = await _dbContext.UserRoles.Where(r => r.UserId == id).FirstAsync();
            var userRole =  allRoles.Where(x => x.Id == userRoleId.RoleId).First();
            var user = await _dbContext.Users.FindAsync(id);

            // Since whatever role is first will be the default shown we sort by that
            allRoles.Sort((x,y) => x.NormalizedName.CompareTo(userRole.NormalizedName));
            List<SelectListItem> userRolesList = new List<SelectListItem>();
            
            foreach (var role in allRoles)
            {
                userRolesList.Add(
                    new SelectListItem
                    {
                        Value = role.NormalizedName.ToString(),
                        Text = role.NormalizedName.ToString()
                    }
                );
            }

            ViewData["Roles"] = userRolesList;

            return View(user);
        }        
        
        [HttpPost]
        public async Task<ActionResult> Edit(IFormCollection form)
        {
            // Gather the updated information and perform the user/role updates as needed.
            var userId = form["Id"].ToString();
            var userEmail = form["Email"].ToString();
            var roleName = form["SelectedRole"].ToString();

            var exisitingUser = await _dbContext.Users.FindAsync(userId);

            // Retrieve all the roles and user-to-role associations for potential updates.
            var rolesList = await _dbContext.Roles.ToListAsync();
            var userToRoleList = await _dbContext.UserRoles.ToListAsync();
            var newUserRoleId = rolesList.Where(x => x.NormalizedName == roleName).First().Id;
            var existingUserToRole = userToRoleList.Where(x => x.UserId == exisitingUser.Id).First();

            var userClaimsList = await _dbContext.UserClaims.ToListAsync();
            var existingUserClaim = userClaimsList.Where(x => x.UserId == userId).First();

            // Since all the values for the following fields within the registration are the same
            // we populate them like so.
            exisitingUser.Email = userEmail;
            exisitingUser.NormalizedEmail = userEmail.ToUpper();            
            exisitingUser.UserName = userEmail;
            exisitingUser.NormalizedUserName = userEmail.ToUpper();

            if (!ModelState.IsValid)
            {
                return View(exisitingUser);
            }

            _dbContext.Users.Update(exisitingUser);
            await _dbContext.SaveChangesAsync();


            // Confirm that there was a role change and update the user's claim and role.
            if (newUserRoleId != existingUserToRole.RoleId)
            {
                _dbContext.UserRoles.Remove(existingUserToRole);
                await _dbContext.SaveChangesAsync();

                existingUserClaim.ClaimValue = roleName;
                existingUserToRole.RoleId = newUserRoleId;

                _dbContext.UserClaims.Update(existingUserClaim);
                _dbContext.UserRoles.Add(existingUserToRole);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}