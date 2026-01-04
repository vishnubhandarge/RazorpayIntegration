using Microsoft.EntityFrameworkCore;

namespace RazorpayIntegration.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
    }
}
