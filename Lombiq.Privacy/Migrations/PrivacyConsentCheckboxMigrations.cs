using Lombiq.Privacy.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyConsentCheckboxMigrations(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterPartDefinitionAsync(nameof(PrivacyConsentCheckboxPart), part => part
            .WithDescription("Provides privacy consent checkbox properties."));

        await contentDefinitionManager.AlterTypeDefinitionAsync(PrivacyConsentCheckbox, type => type
            .WithPart(nameof(PrivacyConsentCheckboxPart))
            .Stereotype("Widget"));

        return 1;
    }
}
