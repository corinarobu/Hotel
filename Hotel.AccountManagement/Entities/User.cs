using Microsoft.AspNetCore.Identity;

namespace Hotel.AccountManagement.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> Roles { get; set; } = [];
        public ICollection<UserBankAccount> BankAccounts { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];

    }
}
