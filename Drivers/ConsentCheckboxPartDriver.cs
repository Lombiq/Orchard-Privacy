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
                _wca.GetContext().CurrentUser == null ? shapeHelper.Parts_ConsentCheckbox_Edit() : null);

        protected override DriverResult Editor(ConsentCheckboxPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            // Model Binder wont get the value from the view (because the HasConsentField will always have a null value)
            // therefore we need to get the posted value from the current HTTP request.

            var wc = _wca.GetContext();
            // This needs to be != true so it covers the null case as well as when the checkbox is not ticked.
            if (wc.HttpContext.Request.Form[$"{nameof(ConsentCheckboxPart)}.{nameof(HasConsent)}"]?.ToLowerInvariant().Contains("true") != true &&
                wc.CurrentUser == null)
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