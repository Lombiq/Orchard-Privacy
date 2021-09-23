using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Activities
{
    public class ValidateConsentCheckboxTask : TaskActivity
    {
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IStringLocalizer T;
        private readonly IHttpContextAccessor _hca;
        private readonly IConsentService _consentService;

        public ValidateConsentCheckboxTask(
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<ValidateConsentCheckboxTask> stringLocalizer,
            IHttpContextAccessor hca,
            IConsentService consentService)
        {
            _updateModelAccessor = updateModelAccessor;
            _hca = hca;
            _consentService = consentService;
            T = stringLocalizer;
        }

        public override string Name => nameof(ValidateConsentCheckboxTask);

        public override LocalizedString DisplayText => T["Validate Consent Checkbox Task"];

        public override LocalizedString Category => T["Validation"];

        public override bool HasEditor => false;

        public override IEnumerable<Outcome> GetPossibleOutcomes(
            WorkflowExecutionContext workflowContext,
            ActivityContext activityContext) =>
            Outcomes(T["Done"], T["Valid"], T["Invalid"]);

        public override async Task<ActivityExecutionResult> ExecuteAsync(
            WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            // If the user has already accepted the privacy statement, it dose not need to validate that form again.
            if (await _consentService.IsUserAcceptedConsentAsync(_hca.HttpContext))
                return Outcomes("Done", "Valid");

            var consentCheckboxName = $"{nameof(ConsentCheckboxPart)}.{nameof(ConsentCheckboxPart.ConsentCheckbox)}";
            var form = _hca.HttpContext.Request.Form;
            var consentCheckboxValue = form[consentCheckboxName].Select(value => bool.Parse(value));
            var isValid = consentCheckboxValue != null && consentCheckboxValue.Contains(true);
            var outcome = isValid ? "Valid" : "Invalid";

            if (!isValid)
            {
                var updater = _updateModelAccessor.ModelUpdater;

                if (updater != null)
                {
                    updater.ModelState.TryAddModelError(
                        consentCheckboxName,
                        T["You have to accept the privacy policy."]);
                }
            }

            return Outcomes("Done", outcome);
        }
    }
}
