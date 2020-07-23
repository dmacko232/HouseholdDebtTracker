using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HouseholdDebtTracker.BLL;
using System.Threading.Tasks;

namespace HouseholdDebtTracker.BlazorUI.Models
{
    /// <summary>
    /// Page model that is used to export (download) people
    /// </summary>
    public class ExportPeople : PageModel
    {
        private IPeopleManager _manager;

        public ExportPeople(IPeopleManager manager) : base() => _manager = manager;

        public async Task<IActionResult> OnGet(string name) {

            var content = await _manager.ExportPeopleAsync();
            return File(content, "application/octet-stream", name);
        }
    }
}