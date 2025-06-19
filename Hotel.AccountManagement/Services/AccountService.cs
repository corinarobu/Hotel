using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountManagementDbContext _context;

        public AccountService(AccountManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserGetDTO>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = await _context.Users
                    .Where(user => !user.Roles.Any(role => role.RoleId == 2))
                    .ToListAsync();

                return users.Select(user => new UserGetDTO
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!
                });
            }

            var searchResults = await _context.Users
        .Where(user => !user.Roles.Any(role => role.RoleId == 2) &&
                      (user.Email!.ToLower().Contains(searchTerm.ToLower()) ||
                       user.UserName!.ToLower().Contains(searchTerm.ToLower())))
        .ToListAsync();


            return searchResults.Select(user => new UserGetDTO
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!
            });
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var bankAccounts = await _context.AccountsUsers
                .Where(ba => ba.UserId == id)
                .ToListAsync();
            _context.AccountsUsers.RemoveRange(bankAccounts);

            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
