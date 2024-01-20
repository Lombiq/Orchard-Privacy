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

namespace Lombiq.Privacy.Activities;

public class ValidatePrivacyConsentCheckboxTask(
    IUpdateModelAccessor updateModelAccessor,
    IStringLocalizer<ValidatePrivacyConsentCheckboxTask> stringLocalizer,
    IHttpContextAccessor hca,
    IPrivacyConsentService consentService) : TaskActivity
{
    private readonly IStringLocalizer T = stringLocalizer;

    public override string Name => nameof(ValidatePrivacyConsentCheckboxTask);

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
        // If the user has already accepted the privacy statement, it doesn't need to validate that form again.
        if (await consentService.IsUserAcceptedConsentAsync(hca.HttpContext))
            return Outcomes("Done", "Valid");

        const string consentCheckboxName = $"{nameof(PrivacyConsentCheckboxPart)}.{nameof(PrivacyConsentCheckboxPart.ConsentCheckbox)}";
        var form = hca.HttpContext.Request.Form;
        var consentCheckboxValue = form[consentCheckboxName].Select(bool.Parse);
        var isValid = consentCheckboxValue.Contains(value: true);
        var outcome = isValid ? "Valid" : "Invalid";

        if (!isValid)
        {
            var updater = updateModelAccessor.ModelUpdater;

            updater?.ModelState.TryAddModelError(consentCheckboxName, T["You have to accept the privacy policy."]);
        }

        return Outcomes("Done", outcome);
    }
}
