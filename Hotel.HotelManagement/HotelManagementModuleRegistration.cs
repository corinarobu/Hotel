using Hotel.HotelManagement.Interfaces;
using Hotel.HotelManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.HotelManagement
{
    public static class HotelManagementModuleRegistration
    {
        public static void AddHotelManagementModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRoomService,RoomService>();
            serviceCollection.AddScoped<IRecomandationService, RecomandationService>();
            serviceCollection.AddScoped<IFAQService, FAQService>();
            serviceCollection.AddScoped<IRatingService, RatingService>();
        }
    }
}
