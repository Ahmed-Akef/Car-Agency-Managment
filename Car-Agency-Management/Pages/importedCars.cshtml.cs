using Car_Agency_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Car_Agency_Management.Pages
{
    // Ignore Antiforgery for simplicity in this demo, or ensure token is passed
    [IgnoreAntiforgeryToken(Order = 1001)] 
    public class importedCarsModel : PageModel
    {
        private DB _db;

        public List<ImportedCar> Cars { get; set; } = new List<ImportedCar>();
        public List<ImportRequest> Requests { get; set; } = new List<ImportRequest>();

        public importedCarsModel()
        {
            _db = new DB();
        }

        public void OnGet()
        {
            Cars = _db.GetAllImportedCars();
            Requests = _db.GetImportRequests();
        }

        public IActionResult OnPostSubmitRequest([FromBody] ImportRequest request)
        {
            if (request == null) return new JsonResult(new { success = false, message = "Invalid data" });

            bool result = _db.AddImportRequest(request);
            return new JsonResult(new { success = result });
        }
    }
}
