
using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Hotel.AccountManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        public static void AddAccountManagementModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IBankAccountService, BankAccountService>();
            serviceCollection.AddScoped<IPaymentService, PaymentService>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<AccountManagementDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var stripeSecretKey = configuration["Stripe:SecretKey"];
                return new PaymentService(context, userManager, stripeSecretKey);
            });
            serviceCollection.AddScoped<IAccountService, AccountService>();


        }
    }
}
