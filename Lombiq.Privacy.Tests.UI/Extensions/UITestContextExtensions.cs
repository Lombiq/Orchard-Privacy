using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Tests.UI.Constants;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Tests.UI.Extensions;

public static class UITestContextExtensions
{
    public static Task ExecutePrivacySampleRecipeDirectlyAsync(this UITestContext context) =>
        context.ExecuteRecipeDirectlyAsync("Lombiq.Privacy.Samples");

    public static Task EnablePrivacyConsentBannerFeatureAsync(this UITestContext context) =>
        context.EnableFeatureDirectlyAsync(FeatureNames.ConsentBanner);

    public static Task EnablePrivacyRegistrationConsentFeatureAsync(this UITestContext context) =>
        context.EnableFeatureDirectlyAsync(FeatureNames.RegistrationConsent);

    /// <remarks>
    /// <para>
    /// This is necessary for Application Insights tracking, even in offline mode.
    /// </para>
    /// </remarks>
    public static Task AcceptPrivacyConsentAsync(this UITestContext context) =>
        context.ClickReliablyOnAsync(By.Id(ElementSelectors.PrivacyConsentAcceptButtonId));

    /// <summary>
    /// Looks for the ASP.NET Core consent cookie. If present, removes it and reloads the page.
    /// </summary>
    public static Task ClearPrivacyConsentCookieAndRefreshAsync(this UITestContext context)
    {
        const string cookieName = ".AspNet.Consent";

        var cookies = context.Driver.Manage().Cookies;
        var consentCookie = cookies
            .AllCookies
            .FirstOrDefault(cookie => cookieName.EqualsOrdinalIgnoreCase(cookie.Name));

        if (consentCookie?.Value.EqualsOrdinalIgnoreCase("yes") != true)
        {
            return Task.CompletedTask;
        }

        cookies.DeleteCookieNamed(cookieName);
        return context.RefreshAsync();
    }

    public static async Task EnablePrivacyConsentBannerFeatureAndAcceptPrivacyConsentAsync(this UITestContext context)
    {
        await context.EnablePrivacyConsentBannerFeatureAsync();
        await context.GoToHomePageAsync(onlyIfNotAlreadyThere: false);
        await context.AcceptPrivacyConsentAsync();
        await context.RefreshAsync();
    }
}
