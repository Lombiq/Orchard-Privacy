using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Drivers;

public sealed class PrivacyConsentCheckboxPartDisplayDriver : ContentPartDisplayDriver<PrivacyConsentCheckboxPart>
{
    private readonly IPrivacyConsentService _consentService;
    private readonly IHttpContextAccessor _hca;

    public PrivacyConsentCheckboxPartDisplayDriver(IPrivacyConsentService consentService, IHttpContextAccessor hca)
    {
        _consentService = consentService;
        _hca = hca;
    }

    public override async Task<IDisplayResult> DisplayAsync(PrivacyConsentCheckboxPart part, BuildPartDisplayContext context) =>
        // If the user has already accepted the privacy statement, it doesn't need to display the checkbox.
        // Except if its configuration explicitly ignores the fact of acceptance.
        (part.ShowAlways ?? false) || !await _consentService.IsUserAcceptedConsentAsync(_hca.HttpContext)
            ? Initialize<PrivacyConsentCheckboxPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => viewModel.ConsentCheckbox = part.ConsentCheckbox).Location("Detail", "Content")
            : null;

    public override IDisplayResult Edit(PrivacyConsentCheckboxPart part, BuildPartEditorContext context) =>
        Initialize<PrivacyConsentCheckboxPartEditViewModel>(GetEditorShapeType(context), m =>
            m.ShowAlways = part.ShowAlways ?? false);

    public override async Task<IDisplayResult> UpdateAsync(PrivacyConsentCheckboxPart part, UpdatePartEditorContext context)
    {
        var viewModel = await context.CreateModelAsync<PrivacyConsentCheckboxPartEditViewModel>(Prefix);
        part.ShowAlways = viewModel.ShowAlways;

        return await EditAsync(part, context);
    }
}
