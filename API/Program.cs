using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddApplicationServices(config); //We have created a new class file where all our services are being added to the builder in order to reduce the 
                                                 //line of code in program CS class.
builder.Services.AddIdentityServices(config); //Same thing we did above, is now being done for identification which refers to all the tokanization processes.

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable CORS
app.UseCors("CorsPolicy");
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); //Enable user authentication using bearer / JWT token provided in the request.

app.UseAuthorization();

app.MapControllers();

app.Run();
