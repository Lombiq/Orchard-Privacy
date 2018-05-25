using Orchard.ContentManagement;
using Orchard.Core.Common.Fields;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentCheckboxSettingsPart;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxSettingsPart : ContentPart
    {
        public TextField ConsentCheckboxTextField => this.AsField<TextField>(ConsentCheckboxText);
    }


    internal static class ConsentCheckboxSettingsPartExtensions
    {
        public static T AsField<T>(this ConsentCheckboxSettingsPart part, string fieldName) where T : ContentField =>
            part.AsField<T>(nameof(ConsentCheckboxSettingsPart), fieldName);
    }
}