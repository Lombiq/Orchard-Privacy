using Orchard.ContentManagement;
using Orchard.Core.Common.Fields;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentBannerSettingsPart;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(ConsentBanner)]
    public class ConsentBannerSettingsPart : ContentPart
    {
        public TextField ConsentBannerTextField => this.AsField<TextField>(ConsentBannerText);
    }


    internal static class ConsentBannerSettingsPartExtensions
    {
        public static T AsField<T>(this ConsentBannerSettingsPart part, string fieldName) where T : ContentField =>
            part.AsField<T>(nameof(ConsentBannerSettingsPart), fieldName);
    }
}