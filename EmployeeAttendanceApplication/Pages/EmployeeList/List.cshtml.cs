using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceApplication.Pages.EmployeeList
{
    public class ListModel : PageModel
    {
        private readonly AppDbContext db;

        public ListModel(AppDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Employee> Employees { get; set; }
        public async Task OnGet()
        {
            Employees = await db.Employee.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var employee = await db.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); 
            }
            else
            {
                db.Employee.Remove(employee);
                await db.SaveChangesAsync();
                return RedirectToPage("List");
            }
        }
    }
}
