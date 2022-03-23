using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailsModel : PageModel
    {
        private readonly IrestaurantData restaurantdata;

        public Restaurant Restaurant { get; set; }
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        public ActionResult OnGet(int Id)
        {
            this.Id = Id;
            Restaurant = restaurantdata.GetById(Id);
            if(Restaurant==null)
            {
                return RedirectToPage("./Notfound");
            }
            return Page();
        }
        public DetailsModel(IrestaurantData restaurantdata)
        {
            this.restaurantdata = restaurantdata;
        }
    }
}
