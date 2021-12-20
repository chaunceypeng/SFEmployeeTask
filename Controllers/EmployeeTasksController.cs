using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFEmployeeTask.Models;

namespace SFEmployeeTask.Controllers
{
    public class EmployeeTasksController : Controller
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public EmployeeTask EmployeeTask { get; set; }

        public EmployeeTasksController(AppDbContext db)
        {
            _db = db;
        }       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            EmployeeTask = new EmployeeTask();
            if (id == null)
            {
                // Create
                return View(EmployeeTask);
            }

            // Update
            EmployeeTask = _db.EmployeeTasks.FirstOrDefault(u => u.Id == id);
            if (EmployeeTask == null)
            {
                return NotFound();
            }
            return View(EmployeeTask);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.EmployeeTasks.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeTask = await _db.EmployeeTasks.FirstOrDefaultAsync(et => et.Id == id);
            if (employeeTask == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.EmployeeTasks.Remove(employeeTask);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Employee Task deleted" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (EmployeeTask.Id == 0)
                {
                    // Create
                    _db.EmployeeTasks.Add(EmployeeTask);
                }
                else
                {
                    // Update
                    _db.EmployeeTasks.Update(EmployeeTask);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(EmployeeTask);
        }

        #endregion
    }
}
