using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid.Models;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyRegistrationConsentSettingsMigrations(
    IContentDefinitionManager contentDefinitionManager,
    IRecipeMigrator recipeMigrator) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterTypeDefinitionAsync(PrivacyRegistrationConsentSettings, type => type
            .WithPart(nameof(LiquidPart))
            .Stereotype("CustomSettings"));

        await recipeMigrator.ExecuteAsync("Recipes/PrivacyRegistrationConsentSettings.recipe.json", this);

        return 1;
    }
}
