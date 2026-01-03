using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Agency_Management.Data;
using System.Collections.Generic;

namespace Car_Agency_Management.Pages
{
    public class MaintenanceReportsModel : PageModel
    {
        private readonly DB _db;
        public List<MaintenanceReportModel> Reports { get; set; } = new List<MaintenanceReportModel>();

        public MaintenanceReportsModel()
        {
            _db = new DB();
        }

        public IActionResult OnGet()
        {
            string role = HttpContext.Session.GetString("UserRole");

            // Allow Admin and Maintenance Staff
            // Note: User previously mentioned "maintenance staff" email starts with "m-" and role logic might be custom.
            // Assuming "Admin" role is standard. "Maintenance Staff" needs verification of role string from User.
            // Previous conversation mentioned "Maintenance Staff" role.
            
            if (role != "Admin" && role != "Maintenance")
            {
               // If role is not strictly defined yet, check email prefix as fallback logic mentioned in previous turns?
               // But standard role check is safer.
               return RedirectToPage("/Index");
            }

            Reports = _db.GetAllMaintenanceReports();
            return Page();
        }
    }
}
