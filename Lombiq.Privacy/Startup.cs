using Lombiq.Privacy.Activities;
using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Drivers;
using Lombiq.Privacy.Filters;
using Lombiq.Privacy.Handlers;
using Lombiq.Privacy.Migrations;
using Lombiq.Privacy.Models;
using Lombiq.Privacy.Navigation;
using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Users.Events;
using OrchardCore.Workflows.Helpers;
using System;
using System.Threading.Tasks;

namespace Lombiq.Privacy;

public class Startup : StartupBase
{
    public override void Configure(
        IApplicationBuilder app,
        IEndpointRouteBuilder routes,
        IServiceProvider serviceProvider) =>
            app.UseCookiePolicy();

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPrivacyConsentService, PrivacyConsentService>();
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => IsConsentNeededAsync(context).GetAwaiter().GetResult();
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
            options.Secure = CookieSecurePolicy.Always;
            options.ConsentCookie.Expiration = new TimeSpan(365, 0, 0, 0);

            options.OnAppendCookie = cookieContext =>
            {
                // Check if the cookie name matches the ones used for Azure AD authentication, or the path matches with
                // the one that Lombiq.Hosting.Tenants.Admin.Login uses.
                if (cookieContext.CookieName.Contains("AspNetCore.OpenIdConnect.Nonce")
                    || cookieContext.CookieName.Contains("AspNetCore.Correlation")
                    || cookieContext.CookieName.Contains("Identity.External")
                    || cookieContext.Context.Features.Get<IHttpRequestFeature>()
                        .Path.Contains("/Lombiq.Hosting.Tenants.Admin.Login/TenantLogin/Index"))
                {
                    // For Azure AD and Lombiq.Hosting.Tenants.Admin.Loginto work with Lombiq.PrivacyPolicy, we need to
                    // set the CookieOptions.SameSite value to SameSiteMode.None. Without this the Azure AD
                    // authentication will fail with the "System.Exception: Correlation failed" error.
                    cookieContext.CookieOptions.SameSite = SameSiteMode.None;
                    cookieContext.CookieOptions.Secure = true;
                    cookieContext.CookieOptions.Domain = cookieContext.Context.Request.Host.Host;
                }
            };
        });
    }

    // This is necessary because IConsentService is not yet available with Dependency Injection at this point.
    private static Task<bool> IsConsentNeededAsync(HttpContext httpContext)
    {
        var consentService = httpContext.RequestServices.GetService<IPrivacyConsentService>();

        if (consentService == null)
        {
            return Task.FromResult(true);
        }

        return consentService.IsConsentNeededAsync(httpContext);
    }
}

[Feature(FeatureNames.ConsentBanner)]
public class ConsentBannerStartup : StartupBase
{
    // This is important because the custom settings menu item override only runs correctly this way.
    public override int Order => -1;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        services.Configure<MvcOptions>((options) =>
            options.Filters.Add(typeof(PrivacyConsentBannerInjectionFilter)));
        services.AddDataMigration<PrivacyConsentBannerSettingsMigrations>();
        services.AddScoped<INavigationProvider, PrivacyConsentBannerSettingsMenu>();
    }
}

[Feature(FeatureNames.RegistrationConsent)]
public class RegistrationConsentStartup : StartupBase
{
    // This is important because the custom settings menu item override only runs correctly this way.
    public override int Order => -1;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MvcOptions>((options) =>
            options.Filters.Add(typeof(RegistrationCheckboxInjectionFilter)));
        services.AddScoped<IRegistrationFormEvents, RegistrationFormEventHandler>();
        services.AddDataMigration<PrivacyRegistrationConsentSettingsMigrations>();
        services.AddScoped<INavigationProvider, PrivacyRegistrationConsentSettingsMenu>();
    }
}

[Feature(FeatureNames.FormConsent)]
public class FormConsentStartup : StartupBase
{
    // This is important because the custom settings menu item override only runs correctly this way.
    public override int Order => -1;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<PrivacyConsentCheckboxPart>()
            .UseDisplayDriver<PrivacyConsentCheckboxPartDisplayDriver>();
        services.AddDataMigration<PrivacyConsentCheckboxMigrations>();
        services.AddDataMigration<PrivacyConsentCheckboxSettingsMigrations>();
        services.AddActivity<ValidatePrivacyConsentCheckboxTask, ValidatePrivacyConsentCheckboxTaskDisplayDriver>();
        services.AddScoped<INavigationProvider, PrivacyConsentCheckboxSettingsMenu>();
    }
}
