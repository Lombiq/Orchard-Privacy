using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly IConsentService _consentService;


        public ConsentCheckboxPartDriver(IConsentService consentService)
        {
            _consentService = consentService;
        }


        protected override DriverResult Editor(ConsentCheckboxPart part, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckbox_Edit", () =>
                !_consentService.UserHasConsent() ? shapeHelper.Parts_ConsentCheckbox_Edit() : null);
    }
}