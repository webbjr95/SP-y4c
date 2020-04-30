using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Areas.Identity.Data;
using SP_Y4C.Data;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    [Authorize]
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
    }
}