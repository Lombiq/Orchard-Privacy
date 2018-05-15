using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(Lombiq_Privacy_Consent_Banner)]
    public class ConsentBannerSettingsPartDriver : ContentPartDriver<ConsentBannerSettingsPart>
    {
        protected override DriverResult Editor(ConsentBannerSettingsPart part, dynamic shapeHelper) =>
            Editor(part, null, shapeHelper);

        protected override DriverResult Editor(ConsentBannerSettingsPart part, IUpdateModel updater, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentBannerSettings_Edit",
                () =>
                {
                    if (updater != null)
                    {
                        updater.TryUpdateModel(part, Prefix, null, null);
                    }

                    return shapeHelper.EditorTemplate(
                        TemplateName: "Parts.ConsentBannerSettings",
                        Model: part,
                        Prefix: Prefix);
                })
            .OnGroup(PrivacySettings);
    }
}