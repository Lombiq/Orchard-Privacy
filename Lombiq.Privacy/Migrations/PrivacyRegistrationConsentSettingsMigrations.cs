using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid.Models;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyRegistrationConsentSettingsMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;
    private readonly IRecipeMigrator _recipeMigrator;

    public PrivacyRegistrationConsentSettingsMigrations(
        IContentDefinitionManager contentDefinitionManager,
        IRecipeMigrator recipeMigrator)
    {
        _contentDefinitionManager = contentDefinitionManager;
        _recipeMigrator = recipeMigrator;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterTypeDefinitionAsync(PrivacyRegistrationConsentSettings, type => type
            .WithPart(nameof(LiquidPart))
            .Stereotype("CustomSettings"));

        await _recipeMigrator.ExecuteAsync("Recipes/PrivacyRegistrationConsentSettings.recipe.json", this);

        return 1;
    }
}
