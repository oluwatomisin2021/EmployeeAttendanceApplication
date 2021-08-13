using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeAttendanceApplication.Pages.EmployeeList
{
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
                employee.Email = Employee.Email;
                employee.Address = Employee.Address;
                employee.PhoneNumber = Employee.PhoneNumber;

                await db.SaveChangesAsync();
                return RedirectToPage("List");
            }
            else
                return Page(); 
        }
    }
}
