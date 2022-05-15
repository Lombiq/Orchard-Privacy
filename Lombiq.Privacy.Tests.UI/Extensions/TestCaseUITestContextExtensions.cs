using Lombiq.Privacy.Tests.UI.Constants;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using OrchardCore.Users.Models;
using Shouldly;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    private const string ExpectedConsentBannerContent = "Our site uses browser cookies to personalize your experience. "
        + "Regarding this issue, please find our information on data control and processing here and our information on "
        + "server logging and usage of so-called 'cookies' here. "
        + "By clicking here You confirm your approval of the data processing activities mentioned in these documents. "
        + "Please note that You may only use this website efficiently if you agree to these conditions.";
    private const string ExpectedRegistrationConsentCheckboxContent = "I've read and agree to the site's privacy policy.";
    private const string ExpectedFormConsentCheckboxContent = "I've read and agree to the site's privacy policy.";

    public static async Task TestPrivacySampleBehaviorAsync(this UITestContext context)
    {
        await context.SelectThemeAsync("TheTheme");
        await context.ExecutePrivacySampleRecipeDirectlyAsync();
        await context.DisablePrivacyConsentBannerFeatureAsync();

        await context.GoToRelativeUrlAsync("/competitor-registration");

        context.VerifyElementTexts(
            By.CssSelector("div.form-check > label"),
            new[] { ExpectedFormConsentCheckboxContent });

        await context.FillInWithRetriesAsync(By.Id("full-name"), TestCompetitor.FullName);
        await context.FillInWithRetriesAsync(By.Id("age"), TestCompetitor.FullName);
        await context.FillInWithRetriesAsync(By.Id("e-mail"), TestCompetitor.FullName);

        await context.ClickReliablyOnSubmitAsync();
    }

    public static async Task TestConsentBannerAsync(this UITestContext context)
    {
        await context.EnablePrivacyConsentBannerFeatureAsync();
        await context.TestConsentBannerContentAsync();
        await context.TestConsentBannerAcceptButtonAsync();
    }

    public static async Task TestConsentBannerContentAsync(this UITestContext context)
    {
        await context.GoToHomePageAsync();

        context.VerifyElementTexts(
            By.CssSelector("#privacy-consent-banner > .modal-content > .modal-body"),
            new[] { ExpectedConsentBannerContent });
    }

    public static async Task TestConsentBannerAcceptButtonAsync(this UITestContext context)
    {
        var privacyConsentAcceptButton = By.Id(ElementSelectors.PrivacyConsentAcceptButtonId);

        await context.GoToHomePageAsync();

        await context.ClickReliablyOnAsync(privacyConsentAcceptButton);
        context.Missing(privacyConsentAcceptButton);

        // Verify persistence.
        context.Refresh();
        context.Missing(privacyConsentAcceptButton);
    }

    public static async Task TestRegistrationConsentCheckboxAsync(this UITestContext context)
    {
        await context.EnablePrivacyConsentBannerFeatureAsync();
        await context.EnablePrivacyRegistrationConsentFeatureAsync();
        await context.SetUserRegistrationTypeAsync(UserRegistrationType.AllowRegistration);

        await context.TestRegistrationConsentCheckboxContentAsync();

        // Go to registration and create a new user
        await context.GoToRegistrationPageAsync();
        await context.FillInWithRetriesAsync(By.Id("UserName"), TestUser.Name);
        await context.FillInWithRetriesAsync(By.Id("Email"), TestUser.Email);
        await context.FillInWithRetriesAsync(By.Id("Password"), TestUser.Password);
        await context.FillInWithRetriesAsync(By.Id("ConfirmPassword"), TestUser.Password);
        await context.SetCheckboxValueAsync(By.Id("RegistrationCheckbox"), isChecked: true);
        await context.ClickReliablyOnSubmitAsync();

        // Login with the created user
        await context.GoToRelativeUrlAsync("/Login");
        await context.FillInWithRetriesAsync(By.Id("UserName"), TestUser.Name);
        await context.FillInWithRetriesAsync(By.Id("Password"), TestUser.Password);
        await context.ClickReliablyOnSubmitAsync();
        (await context.GetCurrentUserNameAsync()).ShouldBe(TestUser.Name);

        // Check that, the consent banner not comes up after login
        await context.GoToHomePageAsync();
        context.Missing(By.Id(ElementSelectors.PrivacyConsentAcceptButtonId));
    }

    public static async Task TestRegistrationConsentCheckboxContentAsync(this UITestContext context)
    {
        await context.GoToRegistrationPageAsync();

        context.VerifyElementTexts(
            By.CssSelector("div.form-check > label"),
            new[] { ExpectedRegistrationConsentCheckboxContent });
    }
}
