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

            services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("https://localhost:4200")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
    });
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();  // Enforces HTTPS
            
            app.UseRouting();  // Enables routing

            app.UseCors("CorsPolicy"); // Enable CORS

            app.UseAuthorization();  // Enables authorization

            app.UseEndpoints(endpoints =>
            {               
                endpoints.MapControllers();       
            });
        }
    }
}
