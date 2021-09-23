using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Drivers
{
    public class ConsentCheckboxPartDisplayDriver : ContentPartDisplayDriver<ConsentCheckboxPart>
    {
        private readonly IConsentService _consentService;
        private readonly IHttpContextAccessor _hca;

        public ConsentCheckboxPartDisplayDriver(IConsentService consentService, IHttpContextAccessor hca)
        {
            _consentService = consentService;
            _hca = hca;
        }

        public override async Task<IDisplayResult> DisplayAsync(ConsentCheckboxPart part, BuildPartDisplayContext context) =>
            // If the user has already accepted the privacy statement, it does not need to be display the checkbox.
            !await _consentService.IsUserAcceptedConsentAsync(_hca.HttpContext)
                ? Initialize<ConsentCheckboxPartViewModel>(
                    GetDisplayShapeType(context),
                    viewModel => viewModel.ConsentCheckbox = part.ConsentCheckbox).Location("Detail", "Content")
                : null;

        public override IDisplayResult Edit(ConsentCheckboxPart part, BuildPartEditorContext context) =>
            View(GetEditorShapeType(context), part);
    }
}
