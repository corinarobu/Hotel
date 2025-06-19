using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Hotel.Server.Controllers.AccountManagement
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, UserManager<User> userManager)
        {
            _paymentService = paymentService;
           
        }
        
    
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentAddDTO paymentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var payment = await _paymentService.ProcessPaymentAsync(
                    userId: paymentDto.Id,
                    bankAccountId: 0, 
                    totalPrice: paymentDto.TotalPrice,
                    startDate: paymentDto.StartDate,
                    endDate: paymentDto.EndDate
                );

                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPaymentsByUser(int userId)
        {
            var payments = await _paymentService.GetPaymentsByUserAsync(userId);
            return Ok(payments);
        }

        [HttpPut("refund/{id}")]
        public async Task<IActionResult> RefundPayment(int id)
        {
            var success = await _paymentService.RefundPaymentAsync(id);
            if (!success)
                return NotFound("Payment not found or already refunded.");

            return Ok("Payment refunded successfully.");
        }
        [HttpGet("has-bank-account/{userId}")]
        public async Task<IActionResult> HasBankAccount(int userId)
        {
            bool hasAccount = await _paymentService.HasBankAccountAsync(userId);
            return Ok(hasAccount);
        }

    }
}
