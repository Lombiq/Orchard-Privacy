using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Handlers
{
    [OrchardFeature(RegistrationConsent)]
    public class RegistrationConsentSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }


        public RegistrationConsentSettingsPartHandler()
        {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<RegistrationConsentSettingsPart>("Site"));

            OnLoaded<RegistrationConsentSettingsPart>((context, part) =>
            {
                if (part.RegistrationConsentTextField.Value == null)
                    part.RegistrationConsentTextField.Value = part.RegistrationConsentTextField.PartFieldDefinition.Settings["TextFieldSettings.DefaultValue"];
            });

        }


        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site") return;

            base.GetItemMetadata(context);

            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Privacy Settings")) { Id = PrivacySettings });
        }
    }
}