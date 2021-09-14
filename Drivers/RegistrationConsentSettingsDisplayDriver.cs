using Lombiq.Privacy.Permissions;
using Lombiq.Privacy.Settings;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.GroupIds;

namespace Lombiq.Privacy.Drivers
{
    public class RegistrationConsentSettingsDisplayDriver : SectionDisplayDriver<ISite, RegistrationConsentSettings>
    {
        private readonly IHttpContextAccessor _hca;
        private readonly IAuthorizationService _authorizationService;

        public RegistrationConsentSettingsDisplayDriver(IHttpContextAccessor hca, IAuthorizationService authorizationService)
        {
            _hca = hca;
            _authorizationService = authorizationService;
        }

        public override async Task<IDisplayResult> EditAsync(RegistrationConsentSettings section, BuildEditorContext context)
        {
            if (!await IsAuthorizedToManagePrivacyConsentAsync())
            {
                return null;
            }

            return Initialize<RegistrationConsentSettingsViewModel>(
                $"{nameof(RegistrationConsentSettings)}_Edit",
                viewModel => viewModel.RegistrationConsentSettingsText = section.RegistrationConsentSettingsText)
                    .Location("Content:2")
                    .OnGroup(PrivacySettings);
        }

        public override async Task<IDisplayResult> UpdateAsync(RegistrationConsentSettings section, BuildEditorContext context)
        {
            if (context.GroupId == PrivacySettings)
            {
                if (!await IsAuthorizedToManagePrivacyConsentAsync())
                {
                    return null;
                }

                var viewModel = new RegistrationConsentSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(viewModel, Prefix);

                section.RegistrationConsentSettingsText = viewModel.RegistrationConsentSettingsText;
            }

            return await EditAsync(section, context);
        }

        private async Task<bool> IsAuthorizedToManagePrivacyConsentAsync()
        {
            var user = _hca.HttpContext?.User;
            return user != null &&
                await _authorizationService.AuthorizeAsync(user, PrivacyConsentPermissions.ManagePrivacyConsent);
        }
    }
}
