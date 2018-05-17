using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly IWorkContextAccessor _wca;

        public Localizer T { get; set; }


        public ConsentCheckboxPartDriver(IWorkContextAccessor wca)
        {
            _wca = wca;

            T = NullLocalizer.Instance;
        }


        protected override DriverResult Editor(ConsentCheckboxPart part, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckbox_Edit", () =>
                _wca.GetContext().CurrentUser ?? shapeHelper.Parts_ConsentCheckbox_Edit());

        protected override DriverResult Editor(ConsentCheckboxPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (!part.HasConsentField.Value.Value)
            {
                var hasNoConsentText = T("Please accept the privacy policy.");

                updater.AddModelError(nameof(part.HasConsentField), hasNoConsentText);
                return ContentShape("Parts_ConsentCheckbox_Edit", () =>
                    shapeHelper.Parts_ConsentCheckbox_Edit(MissingConsent: hasNoConsentText));
            }

            return Editor(part, shapeHelper);
        }
    }
}