using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IrestaurantData irestaurantdata;
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public DeleteModel(IrestaurantData irestaurantdata)
        {
            this.irestaurantdata = irestaurantdata;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            irestaurantdata.Delete(id);
            return RedirectToPage("./List");
            
        }
    }
}
