using Lombiq.Privacy.Constants;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Tests.UI.Extensions;

public static class UITestContextExtensions
{
    public static Task ExecutePrivacySampleRecipeDirectlyAsync(this UITestContext context) =>
        context.ExecuteRecipeDirectlyAsync("Lombiq.Privacy.Samples.Content");

    public static Task EnablePrivacyConsentBannerFeatureAsync(this UITestContext context) =>
        context.EnableFeatureDirectlyAsync(FeatureNames.ConsentBanner);

    public static Task EnablePrivacyRegistrationConsentFeatureAsync(this UITestContext context) =>
        context.EnableFeatureDirectlyAsync(FeatureNames.RegistrationConsent);
}
