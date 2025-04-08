using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using TicketsMVC.Services;

namespace TicketsMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure API base URL from appsettings.json
            builder.Services.AddHttpClient("TicketsAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
            });

            // Register API service (consolidated service for all API operations)
            builder.Services.AddScoped<ApiService>();
            builder.Services.AddScoped<ITicketService>(provider => provider.GetRequiredService<ApiService>());
            builder.Services.AddScoped<IUserService>(provider => provider.GetRequiredService<ApiService>());
            builder.Services.AddScoped<ICategoryService>(provider => provider.GetRequiredService<ApiService>());
            builder.Services.AddScoped<IUrgencyService>(provider => provider.GetRequiredService<ApiService>());
            builder.Services.AddScoped<IImportanceService>(provider => provider.GetRequiredService<ApiService>());
            builder.Services.AddScoped<IRoleService>(provider => provider.GetRequiredService<ApiService>());

            // Add authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Account/Login";
                  options.LogoutPath = "/Account/Logout";
                  options.AccessDeniedPath = "/Account/AccessDenied";
                  options.ExpireTimeSpan = TimeSpan.FromHours(8);
              });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
