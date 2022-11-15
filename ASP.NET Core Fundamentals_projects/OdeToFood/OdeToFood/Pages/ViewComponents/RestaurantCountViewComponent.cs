using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;

namespace OdeToFood.Pages.ViewComponents
{
    public class RestaurantCountViewComponent:ViewComponent
    {
        private readonly IrestaurantData irestaurantdata;

        public RestaurantCountViewComponent(IrestaurantData irestaurantdata)
        {
            this.irestaurantdata = irestaurantdata;
        }

        public IViewComponentResult Invoke()
        {
            var count =irestaurantdata.GetCountOfRestaurants();
            return View(count);
        }
    }
}
