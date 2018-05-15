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
        private readonly ICookieService _cookieService;


        public ConsentCheckboxValidator(IValidationRuleFactory validationRuleFactory, ICookieService cookieService)
        {
            _validationRuleFactory = validationRuleFactory;
            _cookieService = cookieService;
        }


        protected override IEnumerable<IValidationRule> GetValidationRules(ConsentCheckbox element)
        {
            if (!_cookieService.UserHasConsent())
                yield return _validationRuleFactory.Create<Mandatory>("You have to agree to the privacy policy.");
        }
    }
}