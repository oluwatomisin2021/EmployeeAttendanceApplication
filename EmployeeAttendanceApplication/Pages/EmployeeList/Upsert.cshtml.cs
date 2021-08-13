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
    public class UpsertModel : PageModel
    {
        private readonly AppDbContext db;

        public UpsertModel(AppDbContext db)
        {
            this.db = db;
        }
        public Employee Employee { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Employee = new Employee();
            if (id == null)
            {
                return Page();
            }
            else
                Employee = await db.Employee.FirstOrDefaultAsync(e => e.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }
            else
                return Page();           
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Employee.Id == 0)
                {
                    db.Employee.Add(Employee);
                }
                else
                    db.Employee.Update(Employee);

                await db.SaveChangesAsync();

                return RedirectToPage("List");     
            }
            return RedirectToPage();
        }
    }
}
