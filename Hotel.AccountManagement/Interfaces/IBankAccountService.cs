using Hotel.AccountManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Interfaces
{
    public interface IBankAccountService
    {
        Task<List<BankAccountGetDTO>> GetBankAccountForUser(int userId);
        Task AddBankAccountAsync(int userId, BankAccountAddDTO bankAccountAddDTO);
    }
}
