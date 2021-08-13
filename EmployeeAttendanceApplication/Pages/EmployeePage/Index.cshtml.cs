using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeAttendanceApplication.Pages.EmployeePage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext db;

        public IndexModel(AppDbContext db)
        {
            this.db = db;
        }
        public Employee Employee { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            Employee = await db.Employee.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page(); 
        }
    }
}
