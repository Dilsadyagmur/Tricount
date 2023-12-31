using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tricount.DAL.Contexts;
using Tricount.Entities.Concrete;
using Tricount.MVC.AutoMapper;
using Tricount.MVC.Extensions;

namespace Tricount.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SqlDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(TricountMapper));
            builder.Services.AddTricountServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

              app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "Dashboard",
                  pattern: "{area:exists}/{controller=exists}/{action=exists}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.MapRazorPages();

            app.Run();
        }
    }
}