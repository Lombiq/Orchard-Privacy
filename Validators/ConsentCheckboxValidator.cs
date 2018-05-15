using Orchard.DynamicForms.Elements;
using Orchard.DynamicForms.Services;
using Orchard.DynamicForms.ValidationRules;
using System.Collections.Generic;

namespace Lombiq.Privacy.Validators
{
    public class ConsentCheckboxValidator : ElementValidator<ConsentCheckbox>
    {
        private readonly IValidationRuleFactory _validationRuleFactory;


        public ConsentCheckboxValidator(IValidationRuleFactory validationRuleFactory)
        {
            _validationRuleFactory = validationRuleFactory;
        }


        protected override IEnumerable<IValidationRule> GetValidationRules(ConsentCheckbox element)
        {
            yield return _validationRuleFactory.Create<Mandatory>("You have to agree to the privacy policy.");
        }
    }
}