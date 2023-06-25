using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.Extensions.Localization;
using OrchardCore.CustomSettings;
using OrchardCore.CustomSettings.Services;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Navigation;

public class PrivacyConsentCheckboxSettingsMenu : INavigationProvider
{
    private readonly CustomSettingsService _customSettingsService;
    private readonly IStringLocalizer T;

    public PrivacyConsentCheckboxSettingsMenu(
        IStringLocalizer<PrivacyConsentCheckboxSettingsMenu> localizer,
        CustomSettingsService customSettingsService)
    {
        T = localizer;
        _customSettingsService = customSettingsService;
    }

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!name.EqualsOrdinalIgnoreCase("admin"))
        {
            return Task.CompletedTask;
        }

        var type = _customSettingsService.GetSettingsType(PrivacyConsentCheckboxSettings);

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

        return Task.CompletedTask;
    }
}
