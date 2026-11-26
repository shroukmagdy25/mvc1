using Microsoft.AspNetCore.Routing.Constraints;
using mvc1.MiddleWares;

namespace mvc1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseMiddleware<RequestTimingMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("hello");
                await next(context);
                Console.WriteLine("bye");
            });

            //app.UseCors();
            app.UseAuthorization();
            //app.UseSession();
            //app.UseAuthentication();

           

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "shop",
                pattern: "{controller}/{action}/{id?}",
                defaults: new
                {
                    controller = "Products",
                    action = "TestRouting"
                },
                constraints: new
                {
                    id = new IntRouteConstraint()
                }
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id:int ?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
