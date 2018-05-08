using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using static Lombiq.Privacy.Constants.PrivacyConstants;

namespace Lombiq.Privacy.Drivers
{
    public class PrivacySettingsPartDriver : ContentPartDriver<PrivacySettingsPart>
    {
        protected override DriverResult Editor(PrivacySettingsPart part, dynamic shapeHelper) => 
            Editor(part, null, shapeHelper);

        protected override DriverResult Editor(PrivacySettingsPart part, IUpdateModel updater, dynamic shapeHelper) => 
            ContentShape("Parts_PrivacySettings_Edit",
                () =>
                {
                    if (updater != null)
                    {
                        updater.TryUpdateModel(part, Prefix, null, null);
                    }

                    return shapeHelper.EditorTemplate(
                        TemplateName: "Parts.PrivacySettings",
                        Model: part,
                        Prefix: Prefix);
                })
            .OnGroup(SettingsGroupId);
    }
}