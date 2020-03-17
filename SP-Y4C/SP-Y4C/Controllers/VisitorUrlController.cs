using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SP_Y4C.Controllers
{
    public class VisitorUrlController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public VisitorUrlController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var urls = await _dbContext.UrlToVisitors.ToListAsync();

            return View(urls);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var urls = await _dbContext.UrlToVisitors.FirstOrDefaultAsync(q => q.Id == id);
            
            if (urls == null)
            {
                return NotFound();
            }
            
            return View(urls);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UrlToVisitor url)
        {
            if (!ModelState.IsValid)
            {
                return View(url);
            }

            var existingUrl = await _dbContext.UrlToVisitors.FirstOrDefaultAsync(q => q.Id == url.Id);

            existingUrl.Url = url.Url;
            existingUrl.UserType = url.UserType;
            existingUrl.LastModifiedAtUtc = DateTime.UtcNow;

            _dbContext.UrlToVisitors.Update(existingUrl);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
