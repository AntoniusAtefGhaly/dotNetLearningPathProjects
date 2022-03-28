using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IrestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            this.db = context;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            Commit();
            return newRestaurant;
        }

        public int Commit()
        {
            db.SaveChanges();
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var res = db.Restaurants.Find(id);
            if(res != null)
            {
                db.Remove(id);
            }
            Commit();
            return res;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return db.Restaurants;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return db.Restaurants.Where(x => x.Name == name);
        }

        public IEnumerable<Restaurant> SearchByName(string key)
        {
            return db.Restaurants.Where(x => x.Name.ToLower().Contains(key.ToLower()));
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var res = db.Restaurants.Find(updatedRestaurant.Id);
            if(res != null)
            {
                var entity=db.Restaurants.Attach(updatedRestaurant);
                entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Commit();
                return updatedRestaurant;
            }
            return res;            
        }
    }
}
