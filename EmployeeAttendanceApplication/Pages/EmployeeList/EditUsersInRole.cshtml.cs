using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.Models;
using EmployeeAttendanceApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeAttendanceApplication.Pages
{
    public class EditUsersInRoleModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

       
        public IdentityRole Roles { get; set; }
        public IList<IdentityUser> Users { get; set; }

        public UserRoleViewModel UserRole { get; set; }
        public EditUsersInRoleModel(AppDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            Roles = await roleManager.FindByIdAsync(id);
            //Users = await userManager.GetUsersInRoleAsync(Roles.Name);
           
            if (Users != null )
            {
                return Page();
            }
            else
            {
                ViewData["PageError"] = "No user has this role";
                return RedirectToPage("~/NotFound"); 
            }
        }
    }
}
