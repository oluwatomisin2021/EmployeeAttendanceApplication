using EmployeeAttendanceApplication.Models;
using EmployeeAttendanceApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAttendanceApplication.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public Login Login { get; set; }

        public LoginModel(AppDbContext db, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(Login.Email, Login.Password, Login.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    var identityUser = await userManager.FindByEmailAsync(Login.Email);
                    var employee = db.Employee.Include(x => x.User).First(x => x.User == identityUser);

                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("EmployeePage/Index", new { id = employee.Id});
                    }
                    else
                        return RedirectToPage(returnUrl);
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page(); 
        }
    } 
}
