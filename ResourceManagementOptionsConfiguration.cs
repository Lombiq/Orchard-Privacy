using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using static Lombiq.Privacy.Constants.ResourceNames;

namespace Lombiq.Privacy
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest _manifest = new();

        static ResourceManagementOptionsConfiguration() =>
            _manifest
                .DefineStyle(ConsentBanner)
                .SetUrl(
                    "~/Lombiq.Privacy/css/lombiq-privacy-consent-banner.min.css",
                    "~/Lombiq.Privacy/css/lombiq-privacy-consent-banner.css");

        public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
    }
}
