using Lombiq.Privacy.Permissions;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.GroupIds;

namespace Lombiq.Privacy.Navigation
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer) => T = localizer;

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (name.EqualsOrdinalIgnoreCase("admin")) return Task.CompletedTask;

            builder.Add(T["Configuration"], configuration => configuration
                .Add(T["Settings"], settings => settings
                    .Add(T["Privacy"], T["Privacy"], demo => demo
                        .AddClass("privacy").Id("privacy")
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = PrivacySettings })
                        .Permission(PrivacyConsentPermissions.ManagePrivacyConsent)
                        .LocalNav())));

            return Task.CompletedTask;
        }
    }
}
