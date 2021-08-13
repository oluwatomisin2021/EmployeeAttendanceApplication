using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAttendanceApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeAttendanceApplication.Pages.EmployeeList
{
    public class EditRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public IdentityRole EditRoles { get; set; }
        public IList<IdentityUser> Users { get; set; }

        public EditRoleModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            EditRoles = await roleManager.FindByIdAsync(id);
            Users = await userManager.GetUsersInRoleAsync(EditRoles.Name);


            if (EditRoles != null)
            {                
                return Page();
            }
            

            return NotFound();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {                
                var role = await roleManager.FindByIdAsync(EditRoles.Id);
                if (role != null)
                {
                    role.Name = EditRoles.Name;
                    await roleManager.UpdateAsync(role);
                } 
                return RedirectToPage("RoleList");
            }
            return Page();
        }
    }


}
