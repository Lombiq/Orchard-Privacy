using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Fields.Fields;
using Piedone.HelpfulLibraries.Contents;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.ConsentCheckboxPart;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPart : ContentPart
    {
        public BooleanField HasConsentField => this.AsField<BooleanField>(HasConsent);
    }


    internal static class ConsentCheckboxPartPartExtensions
    {
        public static T AsField<T>(this ConsentCheckboxPart part, string fieldName) where T : ContentField =>
            part.AsField<T>(nameof(ConsentCheckboxPart), fieldName);
    }
}