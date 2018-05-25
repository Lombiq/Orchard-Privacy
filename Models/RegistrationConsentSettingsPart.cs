using Orchard.ContentManagement;
using Orchard.Core.Common.Fields;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents;
using static Lombiq.Privacy.Constants.FeatureNames;
using static Lombiq.Privacy.Constants.FieldNames.RegistrationConsentSettingsPart;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(RegistrationConsent)]
    public class RegistrationConsentSettingsPart : ContentPart
    {
        public TextField RegistrationConsentTextField => this.AsField<TextField>(RegistrationConsentText);
    }


    internal static class RegistrationConsentSettingsPartExtensions
    {
        public static T AsField<T>(this RegistrationConsentSettingsPart part, string fieldName) where T : ContentField =>
            part.AsField<T>(nameof(RegistrationConsentSettingsPart), fieldName);
    }
}