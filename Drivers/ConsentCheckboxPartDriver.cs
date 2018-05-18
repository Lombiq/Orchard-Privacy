using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentCheckboxPart;

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
            // Model Binder wont get the value from the view (because the HasConsentField will always has null value)
            // therefore we need to check the posted value from the current HTTP request.
            if (!_wca.GetContext().HttpContext.Request.Form[$"{nameof(ConsentCheckboxPart)}.{nameof(HasConsent)}"].ToLowerInvariant().Contains("true"))
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