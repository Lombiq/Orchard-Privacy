using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Constants
{
    [OrchardFeature(ConsentBanner)]
    public class FieldNames
    {
        public static class ConsentBannerSettingsPart
        {
            public const string ConsentBannerText = nameof(ConsentBannerText);
        }
    }
}