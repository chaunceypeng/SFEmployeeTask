using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFEmployeeTask.Models;

namespace SFEmployeeTask.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public Employee Employee { get; set; }

        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Employee = new Employee();
            if (id == null)
            {
                // Create
                return View(Employee);
            }

            // Update
            Employee = _db.Employees.FirstOrDefault(u => u.Id == id);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Employees.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(em => em.Id == id);
            if (employee == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Employee deleted" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Employee.Id == 0)
                {
                    // Create
                    _db.Employees.Add(Employee);
                }
                else
                {
                    // Update
                    _db.Employees.Update(Employee);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Employee);
        }

        #endregion
    }
}
