using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(RegistrationConsent)]
    public class RegistrationConsentSettingsPart : ContentPart
    {
        public bool EnablePrivacyCheckboxOnRegistrationPage
        {
            get { return this.Retrieve(x => x.EnablePrivacyCheckboxOnRegistrationPage, true); }
            set { this.Store(x => x.EnablePrivacyCheckboxOnRegistrationPage, value); }
        }
    }
}