using Lombiq.Privacy.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Migrations
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckBoxMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(nameof(ConsentCheckboxPart), part => part
                .WithDescription("Attach to a content type to provide Privacy Consent checkbox.")
                .Attachable());

            return 1;
        }
    }
}