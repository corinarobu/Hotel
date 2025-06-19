using Hotel.AccountManagement.Entities;
using Hotel.BookManagement.DTOs;
using Hotel.BookManagement.Interfaces;
using Hotel.BookManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class BookManagementModuleRegistration
{
    public static void AddBookManagementModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBookService, BookService>();
      
    }
}
