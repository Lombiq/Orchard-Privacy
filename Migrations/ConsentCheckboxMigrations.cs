using Lombiq.Privacy.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations
{
    public class ConsentCheckboxMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ConsentCheckboxMigrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(ConsentCheckboxPart), part => part
                .WithDescription("Provides privacy consent checkbox properties."));

            _contentDefinitionManager.AlterTypeDefinition(ConsentCheckbox, type => type
                .WithPart(nameof(ConsentCheckboxPart))
                .Stereotype("Widget"));

            return 1;
        }
    }
}
