using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Tests.UI.Constants;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
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

    public static Task AcceptPrivacyConsentAsync(this UITestContext context) =>
        // For Application Insights tracking to be enabled, even in offline mode, this needs to be run.
        context.ClickReliablyOnAsync(By.Id(ElementSelectors.PrivacyConsentAcceptButtonId));

    public static async Task EnablePrivacyConsentBannerFeatureAndAcceptPrivacyConsentAsync(this UITestContext context)
    {
        await context.EnablePrivacyConsentBannerFeatureAsync();
        await context.GoToHomePageAsync();
        await context.AcceptPrivacyConsentAsync();
        context.Refresh();
    }
}
