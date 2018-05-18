using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.EditorGroupIds;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Handlers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }


        public ConsentCheckboxSettingsPartHandler()
        {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<ConsentCheckboxSettingsPart>("Site"));

            // The TextField won't get its default value by setting it during migration using TextFieldSettings,
            // therefore we need to set the default value here.
            OnLoaded<ConsentCheckboxSettingsPart>((context, part) =>
            {
                if (part.ConsentCheckboxTextField.Value == null)
                    part.ConsentCheckboxTextField.Value = part.ConsentCheckboxTextField.PartFieldDefinition.Settings["TextFieldSettings.DefaultValue"];
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