namespace EShop.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using EShop.Data;
    using EShop.Data.Seeding;
    using EShop.Services;
    using Eshop.Services.Cloudinary;
    using EShop.Services.Data.Orders;
    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Services.Mapping;
    using EShop.Web.Infrastructure.Extensions;
    using EShop.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using EShop.Services.Data.Photos;
    using Ganss.XSS;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabase(configuration)
                .AddIdentity()
                .ConfigureCookiePolicyOptions()
                .AddControllersWithAutoAntiforgeryTokenAttribute()
                .AddRepositories()
                .AddRazorPagesExtension()
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddSingleton(configuration);

            AddApplicationServices(services);
            AddCloudinaryConfiguration(services, configuration);
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            // Application services
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IHtmlSanitizer, HtmlSanitizer>();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
        }

        private static void AddCloudinaryConfiguration(
            IServiceCollection services,
            IConfiguration configuration)
            => services.AddSingleton(CloudinaryConfiguration(configuration));

        private static Cloudinary CloudinaryConfiguration(IConfiguration configuration)
        {
            var cloudinaryCredentials = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            return new Cloudinary(cloudinaryCredentials);
        }
    }
}
