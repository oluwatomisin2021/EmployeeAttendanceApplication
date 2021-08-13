using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceApplication.Pages.EmployeeList
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> userManager;

        public CreateModel(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public void OnGet()
        { 
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {                               

                var user = new IdentityUser()
                {
                    UserName = Employee.Email,
                    Email = Employee.Email

                };

                var result = await userManager.CreateAsync(user, "Password123#");
                //get a link

                if (result.Succeeded)
                {
                     
                    Employee.User = user;
                    await db.Employee.AddAsync(Employee);
                    await db.SaveChangesAsync();
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
                return RedirectToPage("List");
            }
            else
                return Page(); 
        }
    }
}
