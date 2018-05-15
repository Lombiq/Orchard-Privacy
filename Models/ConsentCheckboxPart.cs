using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Models
{
    [OrchardFeature(Lombiq_Privacy_Form_Consent)]
    public class ConsentCheckboxPart : ContentPart { }
}