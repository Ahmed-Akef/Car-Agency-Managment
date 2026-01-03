using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Agency_Management.Data;
using System.Collections.Generic;

namespace Car_Agency_Management.Pages
{
    public class ImportRequestsModel : PageModel
    {
        private readonly DB _db;
        public List<ImportRequest> Requests { get; set; } = new List<ImportRequest>();

        public ImportRequestsModel()
        {
            _db = new DB();
        }

        public IActionResult OnGet()
        {
            // Security Check
            string role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            Requests = _db.GetImportRequests();
            return Page();
        }
    }
}
