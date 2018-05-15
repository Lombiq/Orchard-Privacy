using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(Lombiq_Privacy_Consent_Banner)]
    public class ConsentBannerSettingsPart : ContentPart
    {
        public bool EnableConsentBanner
        {
            get { return this.Retrieve(x => x.EnableConsentBanner, true); }
            set { this.Store(x => x.EnableConsentBanner, value); }
        }
    }
}