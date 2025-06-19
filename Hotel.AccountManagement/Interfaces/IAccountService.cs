using Hotel.AccountManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<UserGetDTO>> SearchUsersAsync(string searchTerm);
        Task<bool> DeleteUserAsync(int id);
    }
}
