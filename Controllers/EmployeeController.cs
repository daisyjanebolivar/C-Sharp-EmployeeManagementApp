using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using System.Linq;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> _employees = new()
        {
            new Employee { Id = 1, Name = "Alice", Position = "Manager", Age = 30 },
            new Employee { Id = 2, Name = "Bob", Position = "Developer", Age = 25 }
        };

        public IActionResult Index() => View(_employees);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            emp.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == emp.Id);
            if (existing == null) return NotFound();
            existing.Name = emp.Name;
            existing.Position = emp.Position;
            existing.Age = emp.Age;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp != null) _employees.Remove(emp);
            return RedirectToAction("Index");
        }
    }
}