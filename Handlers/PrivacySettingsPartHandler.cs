using Lombiq.Privacy.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.EditorGroupIds;

namespace Lombiq.Privacy.Handlers
{
    public class PrivacySettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }


        public PrivacySettingsPartHandler()
        {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<PrivacySettingsPart>("Site"));
        }


        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site") return;

            base.GetItemMetadata(context);

            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Privacy Settings")) { Id = PrivacySettings });
        }
    }
}