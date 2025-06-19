using Hotel.AccountManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
