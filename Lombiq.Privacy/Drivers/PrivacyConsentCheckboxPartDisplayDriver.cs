using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Drivers;

public class PrivacyConsentCheckboxPartDisplayDriver(IPrivacyConsentService consentService, IHttpContextAccessor hca) : ContentPartDisplayDriver<PrivacyConsentCheckboxPart>
{
    public override async Task<IDisplayResult> DisplayAsync(PrivacyConsentCheckboxPart part, BuildPartDisplayContext context) =>
        // If the user has already accepted the privacy statement, it doesn't need to display the checkbox.
        // Except if its configuration explicitly ignores the fact of acceptance.
        (part.ShowAlways ?? false) || !await consentService.IsUserAcceptedConsentAsync(hca.HttpContext)
            ? Initialize<PrivacyConsentCheckboxPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => viewModel.ConsentCheckbox = part.ConsentCheckbox).Location("Detail", "Content")
            : null;

    public override IDisplayResult Edit(PrivacyConsentCheckboxPart part, BuildPartEditorContext context) =>
        Initialize<PrivacyConsentCheckboxPartEditViewModel>(GetEditorShapeType(context), m =>
            m.ShowAlways = part.ShowAlways ?? false);

    public override async Task<IDisplayResult> UpdateAsync(
        PrivacyConsentCheckboxPart part,
        IUpdateModel updater,
        UpdatePartEditorContext context)
    {
        var viewModel = new PrivacyConsentCheckboxPartEditViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.ShowAlways = viewModel.ShowAlways;
        }

        return await EditAsync(part, context);
    }
}
