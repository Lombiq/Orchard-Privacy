using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid.Models;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations
{
    public class ConsentCheckboxSettingsMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;

        public ConsentCheckboxSettingsMigrations(
            IContentDefinitionManager contentDefinitionManager,
            IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
        }

        public async Task<int> CreateAsync()
        {
            _contentDefinitionManager.AlterTypeDefinition(ConsentCheckboxSettings, type => type
                .WithPart(nameof(LiquidPart))
                .Stereotype("CustomSettings"));

            await _recipeMigrator.ExecuteAsync("Recipes/consent-checkbox-settings.recipe.json", this);

            return 1;
        }
    }
}
