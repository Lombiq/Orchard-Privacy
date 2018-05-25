using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxSettingsPartDriver : ContentPartDriver<ConsentCheckboxSettingsPart>
    {
        protected override DriverResult Editor(ConsentCheckboxSettingsPart part, dynamic shapeHelper) =>
            Editor(part, null, shapeHelper);

        protected override DriverResult Editor(ConsentCheckboxSettingsPart part, IUpdateModel updater, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckboxSettings_Edit", () =>
            {
                updater?.TryUpdateModel(part, Prefix, null, null);

                return shapeHelper.EditorTemplate(
                    TemplateName: "Parts/ConsentCheckboxSettings",
                    Model: part,
                    Prefix: Prefix);
            })
            .OnGroup(PrivacySettings);
    }
}