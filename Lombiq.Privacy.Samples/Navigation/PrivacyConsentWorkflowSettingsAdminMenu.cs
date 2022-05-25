using Lombiq.Privacy.Samples.Constants;
using Microsoft.Extensions.Localization;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Workflows.Controllers;
using OrchardCore.Workflows.Services;
using System;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Navigation;

public class PrivacyConsentWorkflowSettingsAdminMenu : INavigationProvider
{
    private readonly IStringLocalizer T;
    private readonly IWorkflowTypeStore _workflowTypeStore;

    public PrivacyConsentWorkflowSettingsAdminMenu(
        IStringLocalizer<PrivacyConsentWorkflowSettingsAdminMenu> localizer,
        IWorkflowTypeStore workflowTypeStore)
    {
        T = localizer;
        _workflowTypeStore = workflowTypeStore;
    }

    public async Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!name.EqualsOrdinalIgnoreCase("admin"))
        {
            return;
        }

        var workflow = await _workflowTypeStore.GetAsync(Ids.RegistrationWorkflowTypeId);
        if (workflow != null)
        {
            builder
                .Add(T["Competitor registration workflow"], builder => builder
                    .Action(
                        nameof(WorkflowTypeController.Edit),
                        typeof(WorkflowTypeController).ControllerName(),
                        new { area = "OrchardCore.Workflows", id = workflow.Id })
                    .LocalNav());
        }
    }
}
