using APPLICATION_DEMO.DAL;
using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using APPLICATION_DEMO.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FluentAssertions.Common;

namespace APPLICATION_DEMO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddDbContext<FoodDBContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
  

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(
                options => {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                }
                ).AddEntityFrameworkStores<FoodDBContext>().AddDefaultTokenProviders(); 
            builder.Services.AddTransient<categoryRepository, CategoryRepository>();
            builder.Services.AddTransient<foodRrpository, FoodRepository>();
            builder.Services.AddScoped<DbInitializer>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>(); 
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
   



           
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

           
            builder.Services.AddScoped(s => SalesCart.GetCart(s));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Add session middleware
          
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication(); // تفعيل المصادقة
            app.UseAuthorization();  // تفعيل التفويض

            

        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "categoryFilter",
                    pattern: "Food/{action}/{category?}",
                    defaults: new { Controller = "FoodController1", Action = "List" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                name: "salesCart",
                 pattern: "SalesCart/{action}/{foodId?}",
                 defaults: new { Controller = "SalesCartController1", Action = "Index" });

                endpoints.MapControllerRoute(
                  name: "orderCheckout",
                  pattern: "Order/Checkout",
                 defaults: new { Controller = "Order", Action = "Checkout" });


            });

          
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                dbInitializer.Seed();
            }

            app.Run();
        }
    }
}
