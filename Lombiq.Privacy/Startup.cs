using Lombiq.HelpfulLibraries.AspNetCore.Extensions;
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
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Users.Events;
using OrchardCore.Users.Models;
using OrchardCore.Workflows.Helpers;
using System;
using System.Threading.Tasks;

namespace Lombiq.Privacy;

public sealed class Startup : StartupBase
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
                // Check if the cookie name matches the ones used for Azure AD authentication.
                if (cookieContext.CookieName.Contains("AspNetCore.OpenIdConnect.Nonce")
                    || cookieContext.CookieName.Contains("AspNetCore.Correlation")
                    || cookieContext.CookieName.Contains("Identity.External"))
                {
                    // For Azure AD to work with Lombiq.PrivacyPolicy, we need to set the CookieOptions.SameSite value
                    // to SameSiteMode.None. Without this the Azure AD authentication will fail with the
                    // "System.Exception: Correlation failed" error.
                    cookieContext.CookieOptions.SameSite = SameSiteMode.None;
                    cookieContext.CookieOptions.Secure = true;
                    cookieContext.CookieOptions.Domain = cookieContext.Context.Request.Host.Host;
                }
                else if (cookieContext.CookieName.Contains("orchauth")) //// #spell-check-ignore-line
                {
                    // We can't set the SameSiteMode to Strict on the Orchard authorization cookie if an external login
                    // feature is enabled since it will come from a different site, so we have to use the default value.
                    cookieContext.CookieOptions.SameSite = SameSiteMode.Lax;
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
public sealed class ConsentBannerStartup : StartupBase
{
    // This is important because the custom settings menu item override only runs correctly this way.
    public override int Order => -1;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        services.AddAsyncResultFilter<PrivacyConsentBannerInjectionFilter>();
        services.AddDataMigration<PrivacyConsentBannerSettingsMigrations>();
        services.AddNavigationProvider<PrivacyConsentBannerSettingsMenu>();
    }
}

[Feature(FeatureNames.RegistrationConsent)]
public sealed class RegistrationConsentStartup : StartupBase
{
    // This is important because the custom settings menu item override only runs correctly this way.
    public override int Order => -1;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDisplayDriver<RegisterUserForm>, RegistrationCheckboxDriver>();
        services.AddScoped<IRegistrationFormEvents, RegistrationFormEventHandler>();
        services.AddDataMigration<PrivacyRegistrationConsentSettingsMigrations>();
        services.AddNavigationProvider<PrivacyRegistrationConsentSettingsMenu>();
    }
}

[Feature(FeatureNames.FormConsent)]
public sealed class FormConsentStartup : StartupBase
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
        services.AddNavigationProvider<PrivacyConsentCheckboxSettingsMenu>();
    }
}
