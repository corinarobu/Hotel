using Hotel.AccountManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(int userId, int bankAccountId, double totalPrice, DateOnly startDate, DateOnly endDate);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);
        Task<bool> RefundPaymentAsync(int paymentId);
        Task<bool> HasBankAccountAsync(int userId);
    }
}
