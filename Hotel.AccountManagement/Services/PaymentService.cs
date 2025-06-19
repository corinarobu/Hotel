using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AccountManagementDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly string _stripeSecretKey;

        public PaymentService(AccountManagementDbContext context, UserManager<User> userManager, string stripeSecretKey)
        {
            _context = context;
            _userManager = userManager;
            _stripeSecretKey = stripeSecretKey;
        }

        public async Task<Payment> ProcessPaymentAsync(int userId, int bankAccountId, double totalPrice, DateOnly startDate, DateOnly endDate)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found");

            if (totalPrice <= 0)
                throw new ArgumentException("Total price must be greater than zero!");

            var payment = new Payment
            {
                UserId = userId,
                BankAccountId = bankAccountId,
                TotalPrice = totalPrice,
                StartDate = startDate,
                EndDate = endDate,
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _context.Payments
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> RefundPaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
                return false;

            payment.TotalPrice = 0;
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> HasBankAccountAsync(int userId)
        {
            return await _context.AccountsUsers.AnyAsync(b => b.UserId == userId);
        }

    }
}
