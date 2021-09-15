using Lombiq.Privacy.Activities;
using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Drivers;
using Lombiq.Privacy.Filters;
using Lombiq.Privacy.Handlers;
using Lombiq.Privacy.Migrations;
using Lombiq.Privacy.Models;
using Lombiq.Privacy.Navigation;
using Lombiq.Privacy.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCore.Users.Events;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using OrchardCore.Workflows.Helpers;
using System;

namespace Lombiq.Privacy
{
    [Feature(FeatureNames.Module)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, PrivacyConsentPermissions>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        }
    }

    [Feature(FeatureNames.ConsentBanner)]
    public class ConsentBannerStartup : StartupBase
    {
        public override void Configure(
            IApplicationBuilder app,
            IEndpointRouteBuilder routes,
            IServiceProvider serviceProvider) =>
                app.UseCookiePolicy();

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.Expiration = new TimeSpan(365, 0, 0, 0);
            });

            services.Configure<MvcOptions>((options) =>
                options.Filters.Add(typeof(ConsentBannerInjectionFilter)));
            services.AddScoped<IDisplayDriver<ISite>, ConsentBannerSettingsDisplayDriver>();
        }
    }

    [Feature(FeatureNames.RegistrationConsent)]
    public class RegistrationConsentStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => IsConsentNeeded(context);
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.Expiration = new TimeSpan(365, 0, 0, 0);
            });

            services.Configure<MvcOptions>((options) =>
                options.Filters.Add(typeof(RegistrationCheckboxInjectionFilter)));
            services.AddScoped<IRegistrationFormEvents, RegistrationFormEventHandler>();
            services.AddScoped<IDisplayDriver<ISite>, RegistrationConsentSettingsDisplayDriver>();
        }

        private static bool IsConsentNeeded(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userService = context.RequestServices.GetService<IUserService>();
                var orchardUser = (User)userService.GetAuthenticatedUserAsync(context.User).GetAwaiter().GetResult();
                return !orchardUser.Has<PrivacyConsent>() || !orchardUser.As<PrivacyConsent>().Accepted;
            }

            return true;
        }
    }

    [Feature(FeatureNames.FormConsent)]
    public class FormConsentStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, ConsentCheckboxSettingsDisplayDriver>();
            services.AddContentPart<ConsentCheckboxPart>()
                .UseDisplayDriver<ConsentCheckboxPartDisplayDriver>();
            services.AddScoped<IDataMigration, ConsentCheckboxMigrations>();
            services.AddActivity<ValidateConsentCheckboxTask, ValidateConsentCheckboxTaskDisplayDriver>();
        }
    }
}
