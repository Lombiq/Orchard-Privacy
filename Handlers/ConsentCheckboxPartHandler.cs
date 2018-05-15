using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Handlers
{
    [OrchardFeature(Lombiq_Privacy_Form_Consent)]
    public class ConsentCheckboxPartHandler : ContentHandler
    {
        public ConsentCheckboxPartHandler() { }
    }
}