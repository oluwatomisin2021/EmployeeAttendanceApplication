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
    public class EditModel : PageModel
    {
        private readonly AppDbContext db;

        public EditModel(AppDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public async Task OnGet(int id)
        {
            Employee = await db.Employee.FindAsync(id); 
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var employee = await db.Employee.FindAsync(Employee.Id);

                employee.FirstName = Employee.FirstName;
                employee.LastName = Employee.LastName;
                employee.PhoneNumber = Employee.PhoneNumber;
                employee.Address = Employee.Address;
                employee.Email = Employee.Email;

                await db.SaveChangesAsync();

                return RedirectToPage("EmployeePage/Index");
            }
            else
                return Page();
        }


    }
}
