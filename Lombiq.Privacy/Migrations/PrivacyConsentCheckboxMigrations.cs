using Lombiq.Privacy.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyConsentCheckboxMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public PrivacyConsentCheckboxMigrations(IContentDefinitionManager contentDefinitionManager) =>
        _contentDefinitionManager = contentDefinitionManager;

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(PrivacyConsentCheckboxPart), part => part
            .WithDescription("Provides privacy consent checkbox properties."));

        await _contentDefinitionManager.AlterTypeDefinitionAsync(PrivacyConsentCheckbox, type => type
            .WithPart(nameof(PrivacyConsentCheckboxPart))
            .Stereotype("Widget"));

        return 1;
    }
}
