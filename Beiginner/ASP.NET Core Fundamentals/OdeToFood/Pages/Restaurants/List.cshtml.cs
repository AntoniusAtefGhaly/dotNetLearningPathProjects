using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data;
using OdeToFood.Core;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        public string msg;
       public IEnumerable<Restaurant> restaurants { get; set; }
        private IConfiguration config { get; set; }
        private readonly IrestaurantData restaurantData;
        [BindProperty(SupportsGet =true)]
        public string SearchKey { get; set; }

        public void OnGet()
        {
            //            msg =config["msg"];
            if(SearchKey != null)
            restaurants = restaurantData.SearchByName(SearchKey);
        }
        public ListModel(IConfiguration config, IrestaurantData irestaurantData)
        {
            this.config = config;
            this.restaurantData = irestaurantData;
            restaurants = restaurantData.GetAll();
        }
    }
}
