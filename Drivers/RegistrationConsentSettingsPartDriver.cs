using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(RegistrationConsent)]
    public class RegistrationConsentSettingsPartDriver : ContentPartDriver<RegistrationConsentSettingsPart>
    {
        protected override DriverResult Editor(RegistrationConsentSettingsPart part, dynamic shapeHelper) =>
            Editor(part, null, shapeHelper);

        protected override DriverResult Editor(RegistrationConsentSettingsPart part, IUpdateModel updater, dynamic shapeHelper) =>
            ContentShape("Parts_RegistrationConsentSettings_Edit", () =>
            {
                updater?.TryUpdateModel(part, Prefix, null, null);

                return shapeHelper.EditorTemplate(
                    TemplateName: "Parts/RegistrationConsentSettings",
                    Model: part,
                    Prefix: Prefix);
            })
            .OnGroup(PrivacySettings);
    }
}