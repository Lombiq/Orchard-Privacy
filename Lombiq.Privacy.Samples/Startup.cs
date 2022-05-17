using Lombiq.Privacy.Samples.Scripting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Scripting;

namespace Lombiq.Privacy.Samples;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGlobalMethodProvider, WorkflowHelperMethodProvider>();
    }
}
