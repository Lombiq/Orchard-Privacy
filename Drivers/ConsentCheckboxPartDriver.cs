using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Notify;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly IWorkContextAccessor _wca;
        private readonly INotifier _notifier;

        public Localizer T { get; set; }


        public ConsentCheckboxPartDriver(IWorkContextAccessor wca, INotifier notifier)
        {
            _wca = wca;
            _notifier = notifier;

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
                _notifier.Error(hasNoConsentText);
            }

            return Editor(part, shapeHelper);
        }
    }
}