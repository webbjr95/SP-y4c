//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SP_Y4C.Models;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace SP_Y4C.Controllers
//{
//    public class QuestionsTableController : Controller
//    {
//        private readonly ApplicationDBContext _context;

//        public QuestionsTableController(ApplicationDBContext context)
//        {
//            _context = context;
//        }
        
//        // GET: /<controller>/
//        public async Task<IActionResult> Index()
//        {
//            //var data = new QuestionsTable
//            //{
//            //    Text = "placeholder for text",
//            //    Description = "placeholder for description",
//            //    CreationDateTime = DateTime.UtcNow,
//            //    Status = false
//            //};

//            //_context.QuestionsTable.Add(data);

//            //await _context.SaveChangesAsync();
//            ////can change questionid to different numbers, as example < 6, > 7, etc
//            //var fromTable = await _context.QuestionsTable.Where(x => x.QuestionID==1).ToListAsync();
//            return View();
//        }
//    }
//}
