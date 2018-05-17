using Lombiq.Privacy.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Fields;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentBannerSettingsPart;

namespace Lombiq.Privacy.Migrations
{
    [OrchardFeature(ConsentBanner)]
    public class ConsentBannerMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(nameof(ConsentBannerSettingsPart), part => part
                .WithField(ConsentBannerText, field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Privacy consent text")
                    .WithSetting("TextFieldSettings.Required", "True")
                    .WithSetting("TextFieldSettings.Hint", "Set the text of privacy consent banner.")
                    .WithSetting("TextFieldSettings.DefaultValue", "<div>Our site uses browser cookies to personalize your experience. " +
                        "Regarding this issue, please find our information on data control and processing <a href='/privacy-policy' target='_blank'>here</a> " +
                        "and our information on server logging and usage of so-called 'cookies' <a href='/privacy-policy' target='_blank'>here</a>. " +
                        "By clicking here You confirm your approval of the data processing activities mentioned in these documents. " +
                        "Please, note that You may only use this website efficiently if you agree to these conditions.</div>")
                    .WithSetting("TextFieldSettings.Flavor", "Html")));

            ContentDefinitionManager.AlterTypeDefinition("Site", type => type.WithPart(nameof(ConsentBannerSettingsPart)));

            return 1;
        }
    }
}