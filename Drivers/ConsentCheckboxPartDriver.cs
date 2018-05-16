using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly IWorkContextAccessor _wca;


        public ConsentCheckboxPartDriver(IWorkContextAccessor wca)
        {
            _wca = wca;
        }


        protected override DriverResult Editor(ConsentCheckboxPart part, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckbox_Edit", () =>
                _wca.GetContext().CurrentUser ?? shapeHelper.Parts_ConsentCheckbox_Edit());
    }
}