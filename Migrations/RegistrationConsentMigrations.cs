using Lombiq.Privacy.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Fields;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.RegistrationConsentSettingsPart;

namespace Lombiq.Privacy.Migrations
{
    [OrchardFeature(RegistrationConsent)]
    public class RegistrationConsentMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(nameof(RegistrationConsentSettingsPart), part => part
                .WithField(RegistrationConsentText, field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Registration privacy consent text")
                    .WithSetting("TextFieldSettings.Required", "True")
                    .WithSetting("TextFieldSettings.Hint", "Set the text of registration consent checkbox.")
                    .WithSetting("TextFieldSettings.DefaultValue", "<div>I've read and agree to the site's <a href='/privacy-policy' target='_blank'>privacy policy</a >.</div>")
                    .WithSetting("TextFieldSettings.Flavor", "Html")));

            ContentDefinitionManager.AlterTypeDefinition("Site", type => type.WithPart(nameof(RegistrationConsentSettingsPart)));

            return 1;
        }
    }
}