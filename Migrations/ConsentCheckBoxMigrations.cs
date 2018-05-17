using Lombiq.Privacy.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Fields;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Fields.Fields;
using Orchard.Fields.Settings;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentCheckboxPart;

namespace Lombiq.Privacy.Migrations
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckBoxMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(nameof(ConsentCheckboxPart), part => part
                .WithDescription("When attached to a content type will provide a privacy consent checkbox.")
                .Attachable()
                .WithField(HasConsent, field => field
                    .OfType(nameof(BooleanField))
                    .WithSetting("BooleanFieldSettings.DefaultValue", "False")
                    .WithSetting("BooleanFieldSettings.SelectionMode", SelectionMode.Checkbox.ToString())
                    .WithSetting("BooleanFieldSettings.Optional", "False")));

            return 1;
        }
    }
}