using System.ComponentModel.DataAnnotations;

namespace RazorpayIntegration.DTOs
{
    public class MakePaymentDTO
    {
        [Required]
        public Decimal amount { get; set; }
    }
}
