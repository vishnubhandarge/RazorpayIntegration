using System.ComponentModel.DataAnnotations;

namespace RazorpayIntegration.DTOs
{
    public class VerifyPaymentDTO
    {
        [Required]
        public string razorpayPaymentId { get; set; }
        [Required]
        public string RazorpayOrderId { get; set; }
        [Required]
        public string RazorpaySignature { get; set; }
    }
}
