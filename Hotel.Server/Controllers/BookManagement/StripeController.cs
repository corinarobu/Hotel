using Hotel.BookManagement.Interfaces;
using Hotel.BookManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;
using Hotel.AccountManagement.Interfaces;
using Hotel.BookManagement.Entities;
using Stripe.Checkout;
using Hotel.AccountManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Server.Controllers.BookManagement
{
    [ApiController]
    [Route("api/stripe")]
    public class StripeController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IBookService _bookService;
        private const string EndpointSecret = "whsec_0fd52fc070db0e044792ec4ff169af5815015a27e23a2af9c15aca19ed10de59";
        private readonly UserManager<User> _userManager;

        public StripeController(IPaymentService paymentService, IBookService bookService, UserManager<User> userManager)
        {
            _paymentService = paymentService;
            _bookService = bookService;
            _userManager = userManager;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSessionAsync([FromQuery] double amount, [FromQuery] int bookingId, [FromQuery] string successUrl, [FromQuery] string cancelUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Hotel Booking"
                    },
                    UnitAmount = (long)(amount * 100),
                },
                Quantity = 1,
            },
        },
                Mode = "payment",
                SuccessUrl = "https://127.0.0.1:61529/succed-payment?bookingId=" + bookingId,
                CancelUrl = "https://127.0.0.1:61529/cancel-payment?bookingId=" + bookingId,
                Metadata = new Dictionary<string, string>
        {
            { "UserId", user.Id.ToString() },
            { "BookingId", bookingId.ToString() }
        }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Ok(new { url = session.Url });
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    EndpointSecret
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if (session?.Metadata.TryGetValue("BookingId", out string? bookingIdStr) == true &&
                        int.TryParse(bookingIdStr, out int bookingId))
                    {
                        await _bookService.UpdatePaymentStatusAsync(bookingId, PaymentStatus.Paid);
                    }
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

    }
}
