using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IrestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant {get; set;}
        public IEnumerable<SelectListItem> Ocusins {get; set;}

        public EditModel(IrestaurantData restaurantData,IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
            Ocusins = this.htmlHelper.GetEnumSelectList<CuisineType>();
        }
        public ActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Restaurant = restaurantData.GetById(id.HasValue?id.Value:0);
            }
            else
            {
                Restaurant=new Restaurant();
            }
            if (Restaurant == null)
                return RedirectToPage("./NotFound");
            return Page();
        }

        public ActionResult OnPost( )
        {
            if (ModelState.IsValid)
            {
                var res  = restaurantData.GetById(Restaurant.Id);
                if (res == null)
                    restaurantData.Add(Restaurant);
                else
                restaurantData.Update(Restaurant);

                return RedirectToPage($"./Details",new 
                {
                     id= Restaurant.Id 
                });
            }
            return Page();
        }
    }
}
