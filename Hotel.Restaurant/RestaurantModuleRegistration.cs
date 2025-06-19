using Hotel.Restaurant.Interfaces;
using Hotel.Restaurant.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant
{
    public static class RestaurantModuleRegistration
    {
        public static void AddRestaurantModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMealService, MealService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
           
        }
    }
}
