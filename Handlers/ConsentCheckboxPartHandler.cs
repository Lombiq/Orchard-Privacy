using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Handlers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartHandler : ContentHandler
    {
        public ConsentCheckboxPartHandler() { }
    }
}