using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.Extensions.Localization;
using OrchardCore.CustomSettings;
using OrchardCore.CustomSettings.Services;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Navigation;

public class PrivacyRegistrationConsentSettingsMenu(
    IStringLocalizer<PrivacyRegistrationConsentSettingsMenu> localizer,
    CustomSettingsService customSettingsService) : INavigationProvider
{
    private readonly IStringLocalizer T = localizer;

    public async Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!name.EqualsOrdinalIgnoreCase("admin"))
        {
            return;
        }

        var type = await customSettingsService.GetSettingsTypeAsync(PrivacyRegistrationConsentSettings);

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
