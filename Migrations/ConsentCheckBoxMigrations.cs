using Lombiq.Privacy.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Lombiq.Privacy.Migrations
{
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