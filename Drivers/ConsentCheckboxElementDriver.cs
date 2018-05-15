using Lombiq.Privacy.Services;
using Orchard.DynamicForms.Elements;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Tokens;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxElementDriver : FormsElementDriver<ConsentCheckbox>
    {
        private readonly ITokenizer _tokenizer;
        private readonly IConsentService _consentService;


        public ConsentCheckboxElementDriver(IFormsBasedElementServices formsServices, ITokenizer tokenizer, IConsentService consentService)
            : base(formsServices)
        {
            _tokenizer = tokenizer;
            _consentService = consentService;
        }


        protected override void OnDisplaying(ConsentCheckbox element, ElementDisplayingContext context)
        {
            context.ElementShape.UserHasConsent = _tokenizer.Replace(_consentService.UserHasConsent().ToString(), context.GetTokenData());
            context.ElementShape.ProcessedName = _tokenizer.Replace("ConsentCheckbox", context.GetTokenData());
            // https://github.com/OrchardCMS/Orchard/issues/4123
            context.ElementShape.ProcessedValue = _tokenizer.Replace("false", context.GetTokenData());
        }
    }
}