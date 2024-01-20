using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid.Models;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyConsentCheckboxSettingsMigrations(
    IContentDefinitionManager contentDefinitionManager,
    IRecipeMigrator recipeMigrator) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterTypeDefinitionAsync(PrivacyConsentCheckboxSettings, type => type
            .WithPart(nameof(LiquidPart))
            .Stereotype("CustomSettings"));

        await recipeMigrator.ExecuteAsync("Recipes/PrivacyConsentCheckboxSettings.recipe.json", this);

        return 1;
    }
}
