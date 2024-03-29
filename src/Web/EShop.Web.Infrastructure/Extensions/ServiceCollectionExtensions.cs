﻿namespace EShop.Web.Infrastructure.Extensions
{
    using EShop.Common;
    using EShop.Data;
    using EShop.Data.Common;
    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using EShop.Data.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<ApplicationDbContext>(
               options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureCookiePolicyOptions(this IServiceCollection services)
            => services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => false;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

        public static IServiceCollection AddControllersWithAutoAntiforgeryTokenAttribute(this IServiceCollection services)
        {
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.CacheProfiles.Add(
                        GlobalConstants.ItemsCacheProfileName,
                        new CacheProfile
                        {
                            Duration = GlobalConstants.CacheExpirationTimeInSeconds,
                            Location = ResponseCacheLocation.Any,
                            NoStore = false,
                            VaryByQueryKeys = new[] { "*" },
                        });
                }).AddRazorRuntimeCompilation();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            return services;
        }

        public static IServiceCollection AddRazorPagesExtension(this IServiceCollection services)
        {
            services.AddRazorPages();
            return services;
        }
    }
}
