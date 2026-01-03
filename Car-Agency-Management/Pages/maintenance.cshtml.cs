using Car_Agency_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Car_Agency_Management.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class maintenanceModel : PageModel
    {
        private DB _db;

        public List<SparePart> SpareParts { get; set; } = new List<SparePart>();

        public maintenanceModel()
        {
            _db = new DB();
        }

        public void OnGet()
        {
            SpareParts = _db.GetSpareParts();
        }

        public class ReportInput
        {
            public string CarModel { get; set; }
            public string IssueType { get; set; }
            public string IssueDescription { get; set; }
        }

        public IActionResult OnPostSubmitReport([FromBody] ReportInput input)
        {
            if (input == null) return new JsonResult(new { success = false, message = "Invalid data" });

            // Hardcoded UserID = 1 for now (or get from session if available)
            // Ideally: int userId = HttpContext.Session.GetInt32("UserID") ?? 0;
            int userId = 1; 

            var result = _db.SubmitMaintenanceReport(userId, input.CarModel, input.IssueType, input.IssueDescription);
            
            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public class AddPartInput
        {
            public string PartName { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
        }

        public IActionResult OnPostAddPart([FromBody] AddPartInput input)
        {
            string role = HttpContext.Session.GetString("UserRole");

            // Allow Admin and Maintenance Staff
            if (role != "Admin" && role != "Maintenance" && role != "Maintenance Staff")
            {
                return new JsonResult(new { success = false, message = "Unauthorized access." });
            }

            if (input == null) return new JsonResult(new { success = false, message = "Invalid data" });

            bool success = _db.AddSparePart(input.PartName, input.Price, input.StockQuantity, input.Category, input.Description);
            
            if (success)
            {
                return new JsonResult(new { success = true, message = "Spare part added successfully!" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Failed to add spare part." });
            }
        }
    }
}
