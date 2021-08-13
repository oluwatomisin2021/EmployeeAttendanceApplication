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
    public class RoleListModel : PageModel
    {        
        private readonly RoleManager<IdentityRole> roleManager;
        
        public IEnumerable<IdentityRole> RolesList { get; set; }
        public RoleListModel(RoleManager<IdentityRole> roleManager)
        {           
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
            RolesList = roleManager.Roles;            
        }
    }
}

