using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;




namespace SP_Y4C.Models
{
    public class GenerateReportController : Controller
    {
        // GET: GenerateReport
        public ActionResult Index()
        {
            return View();
        }

        // GET: GenerateReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenerateReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerateReport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenerateReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenerateReport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenerateReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerateReport/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}