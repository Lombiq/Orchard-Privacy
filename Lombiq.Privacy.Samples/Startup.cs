using Lombiq.Privacy.Samples.Navigation;
using Lombiq.Privacy.Samples.Scripting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Scripting;

namespace Lombiq.Privacy.Samples;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGlobalMethodProvider, WorkflowHelperMethodProvider>();

        services.AddNavigationProvider<PrivacySamplesNavigationProvider>();
    }
}
