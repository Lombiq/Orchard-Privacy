using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Drivers;

public class PrivacyConsentCheckboxPartDisplayDriver : ContentPartDisplayDriver<PrivacyConsentCheckboxPart>
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
        !await _consentService.IsUserAcceptedConsentAsync(_hca.HttpContext)
            ? Initialize<PrivacyConsentCheckboxPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => viewModel.ConsentCheckbox = part.ConsentCheckbox).Location("Detail", "Content")
            : null;

    public override IDisplayResult Edit(PrivacyConsentCheckboxPart part, BuildPartEditorContext context) =>
        View(GetEditorShapeType(context), part);
}
