using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Website.Data;

namespace Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       options.LoginPath = "/Account/Login";
                       options.LogoutPath = "/Account/Logout";
                       options.SlidingExpiration = true;
                       options.ExpireTimeSpan = TimeSpan.FromHours(2);
                       options.Cookie.HttpOnly = true;
                   });

            // Allows for dependacy injection
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<ICardManager, CardManager>();
            builder.Services.AddScoped<IBoosterManager, BoosterManager>();
            builder.Services.AddScoped<IElementManager, ElementManager>();
            builder.Services.AddScoped<IAbilityManager, AbilityManager>();
            builder.Services.AddScoped<IAltArtManager, AltArtManager>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
