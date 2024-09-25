using System.Net.Security;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace YourAppNamespace
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method is called by the runtime to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services like controllers, Razor pages, or others
            services.AddControllers();
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddControllersWithViews();  // For MVC apps
            // For API-only apps, you might just use:
            // services.AddControllers();

            // Register any additional services your app needs
            // Example: services.AddDbContext<YourDbContext>();
        }

        // This method is called by the runtime to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Development-specific middleware
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Middleware for production (e.g., exception handler, HSTS, etc.)
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();  // Enforces HTTPS
            app.UseStaticFiles();  // Serves static files (e.g., .css, .js, etc.)

            app.UseRouting();  // Enables routing

            app.UseAuthorization();  // Enables authorization

            // Define the endpoint routes for the app (e.g., MVC routes)
            app.UseEndpoints(endpoints =>
            {
                // Default route for MVC app
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // For API apps, you might use:
                // endpoints.MapControllers();
            });
        }
    }
}
