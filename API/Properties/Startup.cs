using System.Net.Security;
using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddControllersWithViews();  // For MVC apps    
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

            app.UseAuthentication();  //Enables to use authentication using the JWT token.

            app.UseAuthorization();  // Enables authorization

            app.UseEndpoints(endpoints =>
            {               
                endpoints.MapControllers();       
            });
        }
    }
}
