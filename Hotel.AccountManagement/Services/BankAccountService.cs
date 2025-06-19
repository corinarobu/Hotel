using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly AccountManagementDbContext _context;
        public BankAccountService(AccountManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<BankAccountGetDTO>> GetBankAccountForUser(int userId)
        {
            return await _context.AccountsUsers
                .Where(b => b.UserId == userId)
                .Select(b => new BankAccountGetDTO
                {
                    Id = b.Id,
                    IBAN=b.IBAN,
                    BankName = b.BankName,
                })
                .ToListAsync();

        }
        public async Task AddBankAccountAsync(int userId, BankAccountAddDTO bankAccountAddDTO)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new Exception("No user found!");
            if (bankAccountAddDTO.BankName.Length < 10) throw new Exception("Bank name must be greater than 10 characters");
            if (bankAccountAddDTO.IBAN.Length < 12) throw new Exception("IBAN must be greater than 10 charaters");
            var bankAccount = new UserBankAccount
            {
                UserId = userId,
                User = user,
                IBAN = bankAccountAddDTO.IBAN,
                BankName = bankAccountAddDTO.BankName,
            };
            _context.AccountsUsers.Add(bankAccount);
            await _context.SaveChangesAsync();
            var addedBankAccount = await _context.AccountsUsers
                 .FirstOrDefaultAsync(b => b.UserId == userId && b.IBAN == bankAccountAddDTO.IBAN)
                 ?? throw new Exception("Failed to add bank account");
        }

    }
}
