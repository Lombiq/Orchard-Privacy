using Lombiq.Privacy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Services;

public class PrivacyConsentService(UserManager<IUser> userManager, IOptions<CookiePolicyOptions> cookiePolicyOptions, IUserService userService)
    : IPrivacyConsentService
{
    public async Task<bool> IsConsentBannerNeededAsync(HttpContext httpContext)
    {
        var consentFeature = httpContext.Features.Get<ITrackingConsentFeature>();
        if (!consentFeature.IsConsentNeeded)
        {
            return false;
        }

        if (httpContext.User.Identity.IsAuthenticated)
        {
            var user = await userService.GetAuthenticatedUserAsync(httpContext.User);
            return user is not User orchardUser || !orchardUser.Has<PrivacyConsent>();
        }

        var cookieConsent = httpContext.Request.Cookies[cookiePolicyOptions.Value.ConsentCookie.Name];
        return cookieConsent == null;
    }

    public async Task<bool> IsConsentNeededAsync(HttpContext httpContext)
    {
        if (httpContext.User.Identity.IsAuthenticated)
        {
            var user = await userService.GetAuthenticatedUserAsync(httpContext.User);

            return
                user is not User orchardUser ||
                !(orchardUser.Has<PrivacyConsent>() && orchardUser.As<PrivacyConsent>().Accepted);
        }

        return true;
    }

    public async Task<bool> IsUserAcceptedConsentAsync(HttpContext httpContext)
    {
        if (httpContext.User.Identity.IsAuthenticated)
        {
            var user = await userService.GetAuthenticatedUserAsync(httpContext.User);

            return
                user is User orchardUser &&
                orchardUser.Has<PrivacyConsent>() && orchardUser.As<PrivacyConsent>().Accepted;
        }

        var cookieConsent = httpContext.Request.Cookies[cookiePolicyOptions.Value.ConsentCookie.Name];
        return !string.IsNullOrEmpty(cookieConsent) && cookieConsent.EqualsOrdinalIgnoreCase("yes");
    }

    public async Task StoreUserConsentAsync(ClaimsPrincipal user) =>
        await StoreUserConsentAsync(await userService.GetAuthenticatedUserAsync(user));

    public async Task StoreUserConsentAsync(IUser user)
    {
        if (user is User orchardUser)
        {
            orchardUser.Put(new PrivacyConsent { Accepted = true });
            await userManager.UpdateAsync(orchardUser);
        }
    }
}
