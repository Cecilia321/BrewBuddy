using BrewBuddy.Interface;
using BrewBuddy.Models;
using BrewBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            //dependenciinjektion - Vi registrere her vore brewbuddy i service, så vi kan bruge den i vores razorpage. 
            builder.Services.AddDbContext<BrewBuddyContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("BrewBuddyContext")));

            //dette registrere repositoriet, så jeg kan bruge det i razorpagen 
            builder.Services.AddScoped<IRepository<CoffieMachine>, CoffieMachineRepository>();

            //
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
