using Lombiq.Privacy.Activities;
using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Drivers;
using Lombiq.Privacy.Filters;
using Lombiq.Privacy.Handlers;
using Lombiq.Privacy.Migrations;
using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using OrchardCore.Users.Events;
using OrchardCore.Workflows.Helpers;
using System;
using System.Threading.Tasks;

namespace Lombiq.Privacy
{
    public class Startup : StartupBase
    {
        public override void Configure(
            IApplicationBuilder app,
            IEndpointRouteBuilder routes,
            IServiceProvider serviceProvider) =>
                app.UseCookiePolicy();

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IConsentService, ConsentService>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => IsConsentNeededAsync(context).GetAwaiter().GetResult();
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.Expiration = new TimeSpan(365, 0, 0, 0);
            });
        }

        // This is necessary because IConsentService is not yet available with Dependency Injection at this point.
        private static Task<bool> IsConsentNeededAsync(HttpContext httpContext)
        {
            var consentService = httpContext.RequestServices.GetService<IConsentService>();
            return consentService.IsConsentNeededAsync(httpContext);
        }
    }

    [Feature(FeatureNames.ConsentBanner)]
    public class ConsentBannerStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
            services.Configure<MvcOptions>((options) =>
                options.Filters.Add(typeof(ConsentBannerInjectionFilter)));
            services.AddScoped<IDataMigration, ConsentBannerSettingsMigrations>();
        }
    }

    [Feature(FeatureNames.RegistrationConsent)]
    public class RegistrationConsentStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>((options) =>
                options.Filters.Add(typeof(RegistrationCheckboxInjectionFilter)));
            services.AddScoped<IRegistrationFormEvents, RegistrationFormEventHandler>();
            services.AddScoped<IDataMigration, RegistrationConsentSettingsMigrations>();
        }
    }

    [Feature(FeatureNames.FormConsent)]
    public class FormConsentStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<ConsentCheckboxPart>()
                .UseDisplayDriver<ConsentCheckboxPartDisplayDriver>();
            services.AddScoped<IDataMigration, ConsentCheckboxMigrations>();
            services.AddScoped<IDataMigration, ConsentCheckboxSettingsMigrations>();
            services.AddActivity<ValidateConsentCheckboxTask, ValidateConsentCheckboxTaskDisplayDriver>();
        }
    }
}
