using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Handlers
{
    [OrchardFeature(ConsentBanner)]
    public class ConsentBannerSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }


        public ConsentBannerSettingsPartHandler()
        {
            T = NullLocalizer.Instance;

            // The TextField won't get its default value by setting it during migration using TextFieldSettings,
            // therefore we need to set the default value here.
            OnLoaded<ConsentBannerSettingsPart>((context, part) =>
            {
                if (part.ConsentBannerTextField.Value == null)
                    part.ConsentBannerTextField.Value = part.ConsentBannerTextField.PartFieldDefinition.Settings["TextFieldSettings.DefaultValue"];
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