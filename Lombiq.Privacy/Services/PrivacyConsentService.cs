using Lombiq.Privacy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using StringExtensions = OrchardCore.Modules.StringExtensions;

namespace Lombiq.Privacy.Services;

public class PrivacyConsentService : IPrivacyConsentService
{
    private readonly UserManager<IUser> _userManager;
    private readonly Lazy<IUserService> _userServiceLazy;

    private readonly IOptions<CookiePolicyOptions> _cookiePolicyOptions;

    public PrivacyConsentService(
        UserManager<IUser> userManager,
        IOptions<CookiePolicyOptions> cookiePolicyOptions,
        Lazy<IUserService> userServiceLazy)
    {
        _userManager = userManager;
        _cookiePolicyOptions = cookiePolicyOptions;
        _userServiceLazy = userServiceLazy;
    }

    public async Task<bool> IsConsentBannerNeededAsync(HttpContext httpContext)
    {
        var consentFeature = httpContext.Features.Get<ITrackingConsentFeature>();
        if (!consentFeature.IsConsentNeeded)
        {
            return false;
        }

        if (httpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userServiceLazy.Value.GetAuthenticatedUserAsync(httpContext.User);
            return user is not User orchardUser || !orchardUser.Has<PrivacyConsent>();
        }

        var cookieConsent = httpContext.Request.Cookies[_cookiePolicyOptions.Value.ConsentCookie.Name];
        return cookieConsent == null;
    }

    public async Task<bool> IsConsentNeededAsync(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userServiceLazy.Value.GetAuthenticatedUserAsync(httpContext.User);

            return
                user is not User orchardUser ||
                !(orchardUser.Has<PrivacyConsent>() && orchardUser.GetOrCreate<PrivacyConsent>().Accepted);
        }

        return true;
    }

    public async Task<bool> IsUserAcceptedConsentAsync(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userServiceLazy.Value.GetAuthenticatedUserAsync(httpContext.User);

            return
                user is User orchardUser &&
                orchardUser.Has<PrivacyConsent>() &&
                orchardUser.GetOrCreate<PrivacyConsent>().Accepted;
        }

        var cookieConsent = httpContext.Request.Cookies[_cookiePolicyOptions.Value.ConsentCookie.Name];
        return !string.IsNullOrEmpty(cookieConsent) && StringExtensions.EqualsOrdinalIgnoreCase(cookieConsent, "yes");
    }

    public async Task StoreUserConsentAsync(ClaimsPrincipal user) =>
        await StoreUserConsentAsync(await _userServiceLazy.Value.GetAuthenticatedUserAsync(user));

    public async Task StoreUserConsentAsync(IUser user)
    {
        if (user is User orchardUser)
        {
            orchardUser.Put(new PrivacyConsent { Accepted = true });
            await _userManager.UpdateAsync(orchardUser);
        }
    }
}
