using Lombiq.Privacy.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using static Lombiq.Privacy.Constants.TypeNames;

namespace Lombiq.Privacy.Migrations;

public class PrivacyConsentCheckboxMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public PrivacyConsentCheckboxMigrations(IContentDefinitionManager contentDefinitionManager) =>
        _contentDefinitionManager = contentDefinitionManager;

    public int Create()
    {
        _contentDefinitionManager.AlterPartDefinition(nameof(PrivacyConsentCheckboxPart), part => part
            .WithDescription("Provides privacy consent checkbox properties."));

        _contentDefinitionManager.AlterTypeDefinition(PrivacyConsentCheckbox, type => type
            .WithPart(nameof(PrivacyConsentCheckboxPart))
            .Stereotype("Widget"));

        return 1;
    }
}
