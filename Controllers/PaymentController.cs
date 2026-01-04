using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using RazorpayIntegration.DTOs;

namespace RazorpayIntegration.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string? RazorApiKey;
        private readonly string? RazorSecret;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            RazorApiKey = _configuration["Razorpay:RazorKey"];
            RazorSecret = _configuration["Razorpay:RazorSecret"];
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentDTO dto)
        {
            RazorpayClient client = new RazorpayClient(RazorApiKey, RazorSecret);
            var options = new Dictionary<string, object>
            {
                { "amount", dto.amount * 100 }, // Amount in paise
                { "currency", "INR" },
                { "receipt", "order_rcptid_11" }
            };
            try
            {
                Order order = client.Order.Create(options);
                return Ok(new { orderId = order["id"], amount = order["amount"], currency = order["currency"] });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = $"Error creating order. {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPayment([FromBody] VerifyPaymentDTO dto)
        {
            RazorpayClient client = new RazorpayClient(RazorApiKey, RazorSecret);

            Dictionary<string, string> attributes = new()
            {
                { "razorpay_payment_id", dto.razorpayPaymentId },
                { "razorpay_order_id",   dto.RazorpayOrderId },
                { "razorpay_signature",  dto.RazorpaySignature }
            };

            try
            {
                Utils.verifyPaymentSignature(attributes);
                return Ok(new { status = "Payment verified successfully" });
            }
            catch (Razorpay.Api.Errors.SignatureVerificationError error)
            {
                return BadRequest(new { status = $"Payment verification failed. {error.Message}" });
            }
        }
    }
}
