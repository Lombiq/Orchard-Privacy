using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Scripting;
using OrchardCore.Workflows.Http.Controllers;
using OrchardCore.Workflows.Http.Models;
using OrchardCore.Workflows.Services;
using System;
using System.Collections.Generic;

namespace Lombiq.Privacy.Samples.Scripting;

public class WorkflowHelperMethodProvider : IGlobalMethodProvider
{
    private readonly GlobalMethod _workflowHttpEventUrlResolve;

    public WorkflowHelperMethodProvider() =>
        _workflowHttpEventUrlResolve = new GlobalMethod
        {
            Name = "workflowHttpEventUrlResolve",
            Method = serviceProvider => (Func<string, string, int, object>)((workflowTypeId, activityId, tokenLifeSpan) =>
            {
                var securityTokenService = serviceProvider.GetRequiredService<ISecurityTokenService>();
                var workflowTypeStore = serviceProvider.GetRequiredService<IWorkflowTypeStore>();

                var workflowType = workflowTypeStore.GetAsync(workflowTypeId).GetAwaiter().GetResult();
                var token = securityTokenService.CreateToken(
                    new WorkflowPayload(workflowType.WorkflowTypeId, activityId),
                    TimeSpan.FromDays(
                        tokenLifeSpan == 0 ? HttpWorkflowController.NoExpiryTokenLifespan : tokenLifeSpan));

                // LinkGenerator.GetPathByAction(...) and UrlHelper.Action(...) not resolves url for
                // HttpWorkflowController.Invoke action.
                // Any ideas are welcome.

                return $"/workflows/Invoke?token={Uri.EscapeDataString(token)}";
            }),
        };

    public IEnumerable<GlobalMethod> GetMethods()
    {
        yield return _workflowHttpEventUrlResolve;
    }
}
