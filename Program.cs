using RazorpayIntegration.Data;
using Scalar.AspNetCore;
namespace RazorpayIntegration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<PaymentDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.MapScalarApiReference(options =>
            {
                options.Title = "This is my Scalar API";
                options.DarkMode = true;
                options.Favicon = "path";
                options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.RestSharp);
                options.HideModels = false;
                options.Layout = ScalarLayout.Modern;
                options.ShowSidebar = true;

                options.Authentication = new ScalarAuthenticationOptions
                {
                    PreferredSecurityScheme = "Bearer"
                };
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
