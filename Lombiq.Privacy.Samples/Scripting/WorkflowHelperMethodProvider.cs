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
    private readonly ISecurityTokenService _securityTokenService;

    private readonly GlobalMethod _workflowHttpEventUrlResolve;

    public WorkflowHelperMethodProvider(ISecurityTokenService securityTokenService)
    {
        _securityTokenService = securityTokenService;

        _workflowHttpEventUrlResolve = new GlobalMethod
        {
            Name = "workflowHttpEventUrlResolve",
            Method = serviceProvider => (Func<string, string, int, object>)((workflowTypeId, activityId, tokenLifeSpan) =>
            {
                var workflowTypeStore = serviceProvider.GetRequiredService<IWorkflowTypeStore>();

                var workflowType = workflowTypeStore.GetAsync(workflowTypeId).GetAwaiter().GetResult();
                var token = _securityTokenService.CreateToken(
                    new WorkflowPayload(workflowType.WorkflowTypeId, activityId),
                    TimeSpan.FromDays(
                        tokenLifeSpan == 0 ? HttpWorkflowController.NoExpiryTokenLifespan : tokenLifeSpan));

                return $"/workflows/Invoke?token={token}";
            }),
        };
    }

    public IEnumerable<GlobalMethod> GetMethods()
    {
        yield return _workflowHttpEventUrlResolve;
    }
}
