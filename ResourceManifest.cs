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
            manifest.DefineScript(JsConsentService).SetUrl("consent-service.es5.min.js", "consent-service.es5.js").SetDependencies(JsCookie);

            manifest.DefineStyle(ConsentBanner).SetUrl("lombiq-privacy-consent-banner.min.css", "lombiq-privacy-consent-banner.css");
            manifest.DefineStyle(ConsentCheckbox).SetUrl("lombiq-privacy-consent-checkbox.min.css", "lombiq-privacy-consent-checkbox.css");
        }
    }
}