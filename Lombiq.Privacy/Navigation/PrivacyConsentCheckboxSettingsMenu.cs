using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.CustomSettings;
using OrchardCore.CustomSettings.Services;
using OrchardCore.Navigation;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Navigation;

public sealed class PrivacyConsentCheckboxSettingsMenu : AdminMenuNavigationProviderBase
{
    private readonly CustomSettingsService _customSettingsService;

    public PrivacyConsentCheckboxSettingsMenu(
        IHttpContextAccessor hca,
        IStringLocalizer<PrivacyConsentCheckboxSettingsMenu> stringLocalizer,
        CustomSettingsService customSettingsService)
            : base(hca, stringLocalizer) =>
        _customSettingsService = customSettingsService;

    public async ValueTask BuildNavigationAsync(NavigationBuilder builder)
    {
        var type = await _customSettingsService.GetSettingsTypeAsync(PrivacyConsentCheckboxSettings);

        if (type != null)
        {
            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Settings"], settings => settings
                        .Add(new LocalizedString(type.DisplayName, type.DisplayName), type.DisplayName.PrefixPosition(), layers => layers
                            .SiteSettings(type.Name)
                            .Permission(Permissions.CreatePermissionForType(type))
                            .Resource(type.Name)
                            .AddClass(type.Name)
                            .Id(type.Name)
                            .LocalNav())));
        }
    }
}
