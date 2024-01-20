using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Lombiq.Privacy.Samples.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Workflows;
using OrchardCore.Workflows.Controllers;
using OrchardCore.Workflows.Services;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Samples.Navigation;

public class PrivacySamplesNavigationProvider(
    IHttpContextAccessor hca,
    IStringLocalizer<PrivacySamplesNavigationProvider> stringLocalizer,
    IAuthorizationService authorizationService,
    IWorkflowTypeStore workflowTypeStore) : MainMenuNavigationProviderBase(hca, stringLocalizer)
{
    protected override Task BuildAsync(NavigationBuilder builder) => builder
        .AddAsync(T["Privacy Samples"], builder => builder
            .Add(T["Competitor registration"], itemBuilder => itemBuilder.Url("/competitor-registration"))
            .AddAsync(T["Admin"], builder => builder
                .AddAsync(T["Workflows"], async builder =>
                {
                    builder
                        .Permission(Permissions.ManageWorkflows);

                    if (await authorizationService.AuthorizeAsync(_hca.HttpContext?.User, Permissions.ManageWorkflows))
                    {
                        var workflow = await workflowTypeStore.GetAsync(Ids.RegistrationWorkflowTypeId);
                        if (workflow != null)
                        {
                            builder.Add(T["Competitor registration workflow"], builder => builder
                                .Action(
                                    nameof(WorkflowTypeController.Edit),
                                    typeof(WorkflowTypeController).ControllerName(),
                                    new { area = "OrchardCore.Workflows", id = workflow.Id })
                                .LocalNav());
                        }
                    }
                })));
}
