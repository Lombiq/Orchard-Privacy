using Orchard;
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
        private readonly IWorkContextAccessor _wca;


        public ConsentCheckboxValidator(IValidationRuleFactory validationRuleFactory, IWorkContextAccessor wca)
        {
            _validationRuleFactory = validationRuleFactory;
            _wca = wca;
        }


        protected override IEnumerable<IValidationRule> GetValidationRules(ConsentCheckbox element)
        {
            if (_wca.GetContext().CurrentUser == null)
                yield return _validationRuleFactory.Create<Mandatory>("You have to agree to the privacy policy.");
        }
    }
}