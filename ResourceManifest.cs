using Orchard.UI.Resources;
using static Lombiq.Privacy.Constants.ResourceNames;

namespace Lombiq.Privacy
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript(JsCookie).SetUrl("vendors/js.cookie.js");

            manifest.DefineStyle(ConsentBanner).SetUrl("lombiq-privacy-consent-banner.min.css", "lombiq-privacy-consent-banner.css");
            manifest.DefineStyle(RegistrationConsent).SetUrl("lombiq-privacy-registration-consent.min.css", "lombiq-privacy-registration-consent.css");
        }
    }
}