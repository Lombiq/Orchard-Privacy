using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(Lombiq_Privacy_Registration_Consent)]
    public class RegistrationConsentSettingsPart : ContentPart
    {
        public bool EnablePrivacyCheckboxOnRegistrationPage
        {
            get { return this.Retrieve(x => x.EnablePrivacyCheckboxOnRegistrationPage); }
            set { this.Store(x => x.EnablePrivacyCheckboxOnRegistrationPage, true); }
        }
    }
}