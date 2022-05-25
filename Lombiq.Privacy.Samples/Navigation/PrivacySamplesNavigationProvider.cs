using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Lombiq.Privacy.Samples.Navigation;

public class PrivacySamplesNavigationProvider : MainMenuNavigationProviderBase
{
    public PrivacySamplesNavigationProvider(
        IHttpContextAccessor hca,
        IStringLocalizer<PrivacySamplesNavigationProvider> stringLocalizer)
        : base(hca, stringLocalizer)
    {
    }

    protected override void Build(NavigationBuilder builder) =>
        builder
            .Add(T["Privacy Samples"], builder => builder
                .Add(T["Competitor registration"], itemBuilder => itemBuilder.Url("/competitor-registration")));
}
