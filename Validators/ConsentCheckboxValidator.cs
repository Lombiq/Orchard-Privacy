using Lombiq.Privacy.Services;
using Orchard.DynamicForms.Elements;
using Orchard.DynamicForms.Services;
using Orchard.DynamicForms.ValidationRules;
using Orchard.Environment.Extensions;
using System.Collections.Generic;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Validators
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxValidator : ElementValidator<ConsentCheckbox>
    {
        private readonly IValidationRuleFactory _validationRuleFactory;
        private readonly IConsentService _consentService;


        public ConsentCheckboxValidator(IValidationRuleFactory validationRuleFactory, IConsentService consentService)
        {
            _validationRuleFactory = validationRuleFactory;
            _consentService = consentService;
        }


        protected override IEnumerable<IValidationRule> GetValidationRules(ConsentCheckbox element)
        {
            if (!_consentService.UserHasConsent())
                yield return _validationRuleFactory.Create<Mandatory>("You have to agree to the privacy policy.");
        }
    }
}